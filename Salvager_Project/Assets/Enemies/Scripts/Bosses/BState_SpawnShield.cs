using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SpawnShield : BossState
{
    [Header("Spawn Shields State")]
    public EnemyShieldHandler shields;
    public float movementSpeed;
    public float movementMaxX;
    public float shieldsSpeed;

    float _direction = 1;

    public override void ExecuteState()
    {
        _currentDuration = stateDuration;
        animator.SetTrigger(animationName);
        shields.StartShields();
        shields.originalPosition = shields.transform.position;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        transform.parent.position += transform.parent.right * movementSpeed * _direction * Time.deltaTime;

        if (transform.parent.position.x <= -movementMaxX)
        {
            _direction = -1;
        }
        else if(transform.parent.position.x >= movementMaxX)
        {
            _direction = 1;
        }

        shields.transform.position += shields.transform.forward * shieldsSpeed * Time.deltaTime;
    }

    public override void FinishState()
    {
        shields.EndShields();

        EventHandler.instance.BossStateFinished();
    }
}
