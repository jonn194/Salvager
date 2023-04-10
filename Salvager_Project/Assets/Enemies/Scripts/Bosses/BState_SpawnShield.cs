using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SpawnShield : BossState
{
    [Header("Spawn Shields State")]
    public Transform shields;
    public Vector3 _originalPos;
    public float movementSpeed;
    public float movementMaxX;
    public float shieldsSpeed;

    float _direction = 1;

    public override void ExecuteState()
    {
        _currentDuration = stateDuration;
        animator.SetTrigger(animationName);
        shields.gameObject.SetActive(true);
        _originalPos = shields.position;
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

        shields.position += shields.forward * shieldsSpeed * Time.deltaTime;
    }

    public override void FinishState()
    {
        shields.transform.position = _originalPos;
        shields.gameObject.SetActive(false);

        EventHandler.instance.BossStateFinished();
    }
}
