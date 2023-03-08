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
        newEnemy.transform.rotation = transform.rotation;
        newEnemy.player = player;
        newEnemy.spawner = this;

        _currentEnemies++;
    }

    Vector3 RandomPosition()
    {
        _randomX = Random.Range(-maxPosition, maxPosition);

        return new Vector3(_randomX, transform.position.y, transform.position.z);
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
