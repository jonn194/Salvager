using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_ShootProtectors : BossState
{
    [Header("Shoot Protectors State")]
    public BossProtectorManager protectors;
    public float movementSpeed = 3;

    Vector3 _originalPosition;
    Vector3 _playerPosition;

    bool _reachedPosition;
    bool _waiting;

    public override void ExecuteState()
    {
        animator.SetTrigger(animationName);
        _originalPosition = protectors.transform.position;
        _playerPosition = player.transform.position;
        _reachedPosition = false;
        _waiting = false;
    }

    public override void UpdateState()
    {
        if(!_reachedPosition)
        {
            Vector3 targetPosition = Vector3.Lerp(protectors.transform.position, _playerPosition, movementSpeed * Time.deltaTime);

            protectors.transform.position = targetPosition;

            if(Vector3.Distance(protectors.transform.position, _playerPosition) <= 0.5f && !_waiting)
            {
                _waiting = true;
                StartCoroutine(ReturnTimer());
            }
        }
        else
        {
            Vector3 targetPosition = Vector3.Lerp(protectors.transform.position, _originalPosition, movementSpeed * Time.deltaTime);

            protectors.transform.position = targetPosition;

            if(Vector3.Distance(protectors.transform.position, _originalPosition) <= 0.5f)
            {
                protectors.transform.position = _originalPosition;
                FinishState();
            }
        }
    }

    IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(1f);
        _reachedPosition = true;
    }

    public override void FinishState()
    {
        StopAllCoroutines();
        EventHandler.instance.BossStateFinished();
    }
}
