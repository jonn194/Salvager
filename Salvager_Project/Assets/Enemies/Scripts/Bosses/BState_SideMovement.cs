using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SideMovement : BossState
{
    [Header("Side Movement State")]
    public float movementSpeed;
    public float maxX = 5;
    public float directionChangeTime = 2;
    public float direction = 1;

    float _currentDirectionTime;

    public override void ExecuteState()
    {
        base.ExecuteState();

        _currentDirectionTime = directionChangeTime;

        float initialDirection = Random.Range(-1, 1);

        if(initialDirection < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        transform.parent.position += transform.parent.right * movementSpeed * direction * Time.deltaTime;
        CheckBoundaries();
        CheckDirectionTimer();
    }

    void CheckBoundaries()
    {
        if(transform.parent.position.x > maxX)
        {
            direction = 1;
        }
        else if(transform.parent.position.x < -maxX)
        {
            direction = -1;
        }
    }

    void CheckDirectionTimer()
    {
        _currentDirectionTime -= Time.deltaTime;

        if(_currentDirectionTime <= 0)
        {
            direction *= -1;
            float randomOffset = Random.Range(0, 0.2f);
            _currentDirectionTime = directionChangeTime + randomOffset;
        }
    }
}
