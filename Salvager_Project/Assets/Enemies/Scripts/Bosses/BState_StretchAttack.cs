using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BState_StretchAttack : BossState
{
    [Header("Stretch Attack State")]
    public float chargeSpeed = 1;
    public float attackSpeed = 4;
    public float holdTime = 2;
    public Vector3 chargePosition;
    public float maxAttackX;
    public float maxAttackZ;

    Vector3 _attackPosition;
    Vector3 _originalPosition;
    Vector3 _originalRotation;

    int _sequence;
    float _currentHoldTime;
    public override void ExecuteState()
    {
        base.ExecuteState();
        _sequence = 0;
        _currentHoldTime = holdTime;
        _originalPosition = transform.parent.position;
        _originalRotation = transform.parent.eulerAngles;
        SetAttackPosition();
    }

    void SetAttackPosition()
    {
        float randomX = Random.Range(-maxAttackX, maxAttackX);

        _attackPosition = new Vector3(randomX, transform.parent.position.y, maxAttackZ);
    }

    public override void UpdateState()
    {
        if(_sequence == 0)
        {
            Move(chargePosition, chargeSpeed);
        }
        else if (_sequence == 1)
        {
            Move(_attackPosition, attackSpeed);
            transform.parent.LookAt(_attackPosition);
        }
        else if (_sequence == 2)
        {
            Hold();
        }
        else if(_sequence == 3)
        {
            Move(_originalPosition, chargeSpeed);

            Vector3 tempRot = Vector3.Lerp(transform.parent.eulerAngles, _originalRotation, 8 * Time.deltaTime);

            transform.parent.eulerAngles = tempRot;
        }
        else if(_sequence >= 4)
        {
            FinishState();
        }
    }

    void Move(Vector3 target, float speed)
    {
        Vector3 targetPos = Vector3.Lerp(transform.parent.position, target, speed * Time.deltaTime);

        transform.parent.position = targetPos;

        if(CheckDistance(target, transform.parent.position, 0.5f))
        {
            _sequence++;
        }
    }

    void Hold()
    {
        _currentHoldTime -= Time.deltaTime;

        if(_currentHoldTime <= 0)
        {
            _sequence++;
            animator.SetBool(animationName, false);
        }
    }
}
