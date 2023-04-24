using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySinusoidal : Enemy
{
    [Header("Sinusoidal")]
    public Animator anim;

    public float verticalSpeed;
    public float horizontalSpeed;
    public float amplitude;
    
    bool _sineDirection = false;

    public override void Start()
    {
        base.Start();
        RandomDirection();
    }

    void RandomDirection()
    {
        float random = Random.Range(0, 99);

        if(random <= 50)
        {
            _sineDirection = true;
        }
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

        if(modifiedTime < 0)
        {
            anim.SetFloat("AnimSpeed", -1);
        }
        else
        {
            anim.SetFloat("AnimSpeed", 1);
        }

        transform.position += (transform.forward * verticalSpeed + transform.right * horizontalSpeed * modifiedTime) * Time.deltaTime;
    }
}
