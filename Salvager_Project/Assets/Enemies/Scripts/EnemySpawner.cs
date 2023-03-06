using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Parameteres")]
    public float maxPosition;
    public float spawnFrequency = 2;
    public int maxEnemies;
    int _currentEnemies;

    [Header("Enemies")]
    public List<EnemiesContainer> objectsToSpawn = new List<EnemiesContainer>();

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

        EnemiesContainer newEnemy = Instantiate(objectsToSpawn[random], transform.parent);
        newEnemy.transform.position = transform.position;
        newEnemy.transform.rotation = transform.rotation;
        newEnemy.spawner = this;

        _currentEnemies++;
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
