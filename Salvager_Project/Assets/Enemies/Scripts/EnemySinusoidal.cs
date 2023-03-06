using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySinusoidal : Enemy
{
    public float verticalSpeed;
    public float horizontalSpeed;
    public float amplitude;
    public bool _sineDirection;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Movement();
    }

    void Movement()
    {
        float modifiedTime = 0;

        if(_sineDirection)
        {
            modifiedTime = Mathf.Sin(Time.time / amplitude);
        }
        else
        {
            modifiedTime = Mathf.Cos(Time.time / amplitude);
        }

        transform.position += (transform.forward * verticalSpeed + transform.right * horizontalSpeed * modifiedTime) * Time.deltaTime;
    }
}
