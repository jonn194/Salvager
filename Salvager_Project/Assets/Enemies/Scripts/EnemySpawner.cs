using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IObservable
{
    [Header("Dependencies")]
    public PlayerStats player;
    public ItemSpawner itemSpawn;

    [Header("Parameteres")]
    public float maxPosition;
    public float maxAngle = 10;
    public float baseSpawnFrequency = 2;
    public float minSpawnFrequency = 0.2f;

    float _currentSpawnFrequency;
    int _currentEnemies;

    [Header("Enemies")]
    public List<Enemy> enemies = new List<Enemy>();
    public List<Boss> bosses = new List<Boss>();
    public Boss bossEpsilon;
    Boss _currentBoss;
    float _randomX;

    [Header("Dificulty")]
    public int enemiesDestroyedByPlayer;
    public int currentDificulty;
    public float frequencyOffset;
    public List<int> enemiesToChange = new List<int>();
    public List<int> maxEnemiesAmounts = new List<int>();
    public List<int> maxEnemyIndex = new List<int>();

    int _dificulty;
    int _maxDificulty;
    int _reinforceLevel;
    Vector3 _screenBoundaries;

    private void Start()
    {
        EventHandler.instance.levelUp += LevelUp;
        _maxDificulty = enemiesToChange.Count;
    }

    public void StartEnemies()
    {
        currentDificulty = 0;
        _currentSpawnFrequency = baseSpawnFrequency;
        _dificulty = 0;
        _reinforceLevel = 0;
        _screenBoundaries = GameManager.instance.CalculateScreenBounds();
        StartCoroutine(SpawnTimer());
    }

    public void StopEnemies()
    {
        StopCoroutine(SpawnTimer());
        gameObject.SetActive(false);
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(_currentSpawnFrequency);

        //check enemies amount of with current dificulty
        if(_currentEnemies <= maxEnemiesAmounts[_dificulty])
        {
            SpawnEnemies();
        }
        StartCoroutine(SpawnTimer());
    }

    void SpawnEnemies()
    {
        //set random enemy according to current dificulty
        int random = Random.Range(0, maxEnemyIndex[_dificulty] + 1);

        Enemy newEnemy = Instantiate(enemies[random], transform.parent);
        newEnemy.transform.position = RandomPosition();
        newEnemy.transform.eulerAngles = RandomRotation(newEnemy.transform.position.x);
        newEnemy.player = player;
        newEnemy.itemSpawner = itemSpawn;
        newEnemy.spawner = this;
        newEnemy.reinforceLevel = _reinforceLevel;
        newEnemy.screenBoundaries = _screenBoundaries;

        _currentEnemies++;
    }

    Vector3 RandomPosition()
    {
        //float tempBoundary = _screenBoundaries.x - maxPosition;
        _randomX = Random.Range(-maxPosition, maxPosition);

        return new Vector3(_randomX, transform.position.y, transform.position.z);
    }

    Vector3 RandomRotation(float xPos)
    {
        float _randomY = 0;

        if(xPos > 0)
        {
            _randomY = Random.Range(-5, maxAngle);
        }
        else
        {
            _randomY = Random.Range(5, -maxAngle);
        }

        _randomY += transform.eulerAngles.y;

        return new Vector3(transform.eulerAngles.x, _randomY, transform.eulerAngles.z);
    }

    public void RemoveEnemy(bool byPlayer)
    {
        _currentEnemies--;

        //if enemy destroyed by player
        if(byPlayer)
        {
            enemiesDestroyedByPlayer++;

            //checks the amount of enemies
            if (enemiesDestroyedByPlayer >= enemiesToChange[_dificulty])
            {
                //create boss
                SpawnBoss();
            }
        }
    }

    void CheckDificulty()
    {
        if(currentDificulty < _maxDificulty)
        {
            _dificulty = currentDificulty;

        }
        else
        {
            ReinforceEnemies();
            _dificulty = _maxDificulty - 1;
        }
    }

    void ReinforceEnemies()
    {
        if((currentDificulty - 1) % _maxDificulty == 0)
        {
            _reinforceLevel++;
        }
    }

    void SpawnBoss()
    {
        //resets destroyed enemies count
        enemiesDestroyedByPlayer = 0;

        EventHandler.instance.BossIncoming();

        Boss newBoss;
        if(currentDificulty != 0 && currentDificulty % 5 == 0)
        {
            newBoss = Instantiate(bossEpsilon, transform.parent);
        }
        else
        {
            int randomBoss = Random.Range(0, bosses.Count);
            newBoss = Instantiate(bosses[randomBoss], transform.parent);
        }

        newBoss.transform.position = transform.position;
        newBoss.transform.rotation = transform.rotation;
        newBoss.player = player;
        newBoss.itemSpawner = itemSpawn;
        newBoss.screenBoundaries = _screenBoundaries;

        _currentBoss = newBoss;

        //stop incoming enemies
        StopAllCoroutines();
    }

    void LevelUp()
    {
        //increase dificulty
        currentDificulty++;

        CheckDificulty();

        //adjust spawn frequency
        if (_currentSpawnFrequency > minSpawnFrequency)
        {
            _currentSpawnFrequency -= frequencyOffset;
        }
        else
        {
            _currentSpawnFrequency = minSpawnFrequency;
        }

        StartCoroutine(SpawnTimer());
    }

    //INTERFACE
    public void ObserverSuscribe()
    {
        EventHandler.instance.levelUp += LevelUp;
    }

    public void ObserverUnsuscribe()
    {
        EventHandler.instance.levelUp -= LevelUp;

        if(_currentBoss != null)
        {
            _currentBoss.ObserverUnsuscribe();
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_screenBoundaries.x, 0.5f, 0.5f));
    }*/
}
