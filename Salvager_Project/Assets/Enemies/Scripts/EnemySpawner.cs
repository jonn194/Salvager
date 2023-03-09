using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Player")]
    public PlayerStats player;

    [Header("Parameteres")]
    public float maxPosition;
    public float maxAngle = 10;
    public float spawnFrequency = 2;
    public int maxEnemies;
    int _currentEnemies;

    [Header("Enemies")]
    public List<Enemy> objectsToSpawn = new List<Enemy>();

    float _randomX;

    public void StartEnemies()
    {
        StartCoroutine(SpawnTimer());
    }

    public void StopEnemies()
    {
        StopCoroutine(SpawnTimer());
        gameObject.SetActive(false);
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnFrequency);
        if(_currentEnemies <= maxEnemies)
        {
            SpawnEnemies();
        }
        StartCoroutine(SpawnTimer());
    }

    void SpawnEnemies()
    {
        int random = Random.Range(0, objectsToSpawn.Count);

        Enemy newEnemy = Instantiate(objectsToSpawn[random], transform.parent);
        newEnemy.transform.position = RandomPosition();
        newEnemy.transform.eulerAngles = RandomRotation(newEnemy.transform.position.x);
        newEnemy.player = player;
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
            _randomY = Random.Range(0, maxAngle);
        }
        else
        {
            _randomY = Random.Range(0, -maxAngle);
        }

        _randomY += transform.eulerAngles.y;

        return new Vector3(transform.eulerAngles.x, _randomY, transform.eulerAngles.z);
    }

    public void RemoveEnemy()
    {
        _currentEnemies--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(maxPosition, 0.5f, 0.5f));
    }
}
