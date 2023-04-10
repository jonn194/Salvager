using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BState_CanonShooting : BossState
{
    [Header("Canon Shooting State")]
    public string shootAnim;
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
        StartCoroutine(PlayShootAnim());

        if (transform.parent.position.x < 0)
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

    IEnumerator PlayShootAnim()
    {
        yield return new WaitForSeconds(canonWeapon.shootTimer);
        animator.SetTrigger(shootAnim);
        StartCoroutine(PlayShootAnim());
    }

    public override void FinishState()
    {
        StopAllCoroutines();
        canonWeapon.StopShooting();
        base.FinishState();
    }
}
