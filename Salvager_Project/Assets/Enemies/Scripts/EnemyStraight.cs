using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraight : Enemy
{
    public float moveSpeed;


    public override void Update()
    {
        base.Update();
        Movement();
    }

    void Movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

}
