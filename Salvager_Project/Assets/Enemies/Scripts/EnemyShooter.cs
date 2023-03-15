using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [Header("Shooter")]
    public float verticalTime;
    public float horizontalTime;

    float _direction = 1;

    public override void Start()
    {
        base.Start();
        
        //fix rotation
        transform.rotation = spawner.transform.rotation;
        
        //radom vertical duration
        RandomVertical();

        StartCoroutine(VerticalMovement());
    }

    void RandomVertical()
    {
        float random = Random.Range(-0.5f, 0.5f);
        verticalTime += random;
    }

    public override void Update()
    {    
    }

    IEnumerator VerticalMovement()
    {
        while(true)
        {
            verticalTime -= Time.deltaTime;

            if(verticalTime < 0)
            {
                StartCoroutine(HorizontalMovement());
                StartCoroutine(DirectionTimer()); 
                yield break;
            }

            BasicMovement();

            yield return null;
        }
    }

    IEnumerator HorizontalMovement()
    {
        while (true)
        {
            transform.position += transform.right * moveSpeed * _direction * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator DirectionTimer()
    {
        yield return new WaitForSeconds(horizontalTime);
        _direction *= -1;
        StartCoroutine(DirectionTimer());
    }

    public override void DestroyEnemy(bool byPlayer)
    {
        StopAllCoroutines();

        base.DestroyEnemy(byPlayer);
    }
}
