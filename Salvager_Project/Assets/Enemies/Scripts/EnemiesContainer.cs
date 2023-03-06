using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public EnemySpawner spawner;

    void Update()
    {
        if(transform.childCount == 0)
        {
            spawner.RemoveEnemy();
            Destroy(gameObject);
        }
    }
}
