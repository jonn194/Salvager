using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BState_CanonShooting : BossState
{
    [Header("Canon Shooting State")]
    public bool useTimer;
    public Shooting canonWeapon;
    public float movementSpeed;
    public float targetX = 7;

    float _direction = 1;
    float _currentTarget = 7;

    public override void ExecuteState()
    {
        base.ExecuteState();

        canonWeapon.StartShooting();

        if(transform.parent.position.x < 0)
        {
            _currentTarget = targetX;
            _direction = -1;
        }
        else
        {
            _currentTarget = -targetX;
            _direction = 1;
        }
    }
    public override void UpdateState()
    {
        if(AnimationStartEnded())
        {
            Movement();

            if (useTimer)
            {
                base.UpdateState();
            }
        }
    }

    void Movement()
    {
        transform.parent.position += transform.parent.right * movementSpeed * _direction * Time.deltaTime;

        if(!useTimer)
        {
            if ((transform.parent.position.x < _currentTarget && _direction > 0) ||
                (transform.parent.position.x > _currentTarget && _direction < 0))
            {
                FinishState();
            }
        }
        else
        {
            if(transform.parent.position.x <= -targetX)
            {
                _direction = -1;
            }
            else if(transform.parent.position.x >= targetX)
            {
                _direction = 1;
            }
        }
        
    }

    public override void FinishState()
    {
        canonWeapon.StopShooting();
        base.FinishState();
    }
}
