using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
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
    float _randomX;

    [Header("Dificulty")]
    public int enemiesDestroyedByPlayer;
    public int currentDificulty;
    public float frequencyOffset;
    public List<int> enemiesToChange = new List<int>();
    public List<int> maxEnemiesAmounts = new List<int>();
    public List<int> maxEnemyIndex = new List<int>();

    private void Start()
    {
        EventHandler.instance.levelUp += LevelUp;
    }

    public void StartEnemies()
    {
        _currentSpawnFrequency = baseSpawnFrequency;
        currentDificulty = 0;
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
        if(_currentEnemies <= maxEnemiesAmounts[currentDificulty])
        {
            SpawnEnemies();
        }
        StartCoroutine(SpawnTimer());
    }

    void SpawnEnemies()
    {
        //set random enemy according to current dificulty
        int random = Random.Range(0, maxEnemyIndex[currentDificulty] + 1);

        Enemy newEnemy = Instantiate(enemies[random], transform.parent);
        newEnemy.transform.position = RandomPosition();
        newEnemy.transform.eulerAngles = RandomRotation(newEnemy.transform.position.x);
        newEnemy.player = player;
        newEnemy.itemSpawner = itemSpawn;
        newEnemy.spawner = this;

        _currentEnemies++;
    }

    Vector3 RandomPosition()
    {
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

            CheckDificulty();
        }
    }

    void CheckDificulty()
    {
        //check if there are more dificulty levels
        if(enemiesToChange.Count > currentDificulty)
        {
            //checks the amount of enemies
            if (enemiesDestroyedByPlayer >= enemiesToChange[currentDificulty])
            {
                //increase dificulty
                currentDificulty++;

                //resets destroyed enemies count
                enemiesDestroyedByPlayer = 0;

                //create boss
                SpawnBoss();
            }
        }
    }

    void SpawnBoss()
    {
        EventHandler.instance.BossIncoming();
        StopCoroutine(SpawnTimer());

        //int random = Random.Range(0, bosses.Count);

        Boss newBoss = Instantiate(bosses[0], transform.parent);
        newBoss.transform.position = transform.position;
        newBoss.transform.rotation = transform.rotation;
        newBoss.player = player;
    }

    void LevelUp()
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(maxPosition, 0.5f, 0.5f));
    }
}
