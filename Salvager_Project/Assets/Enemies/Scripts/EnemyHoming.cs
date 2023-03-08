using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoming : Enemy
{
    public float moveSpeed;

    public override void Update()
    {
        base.Update();

        transform.LookAt(player.transform);
        Movement();
        
    }

    void Movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
