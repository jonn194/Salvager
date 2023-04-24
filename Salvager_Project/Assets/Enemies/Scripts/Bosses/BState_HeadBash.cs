using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_HeadBash : BossState
{
    [Header("Head Bash State")]
    public float bashSpeed;
    public float bashWaitTime = 1f;
    public float bashLimitX = 8;
    public float bashLimitZ = 20;

    public float returnSpeed = 8;
    public float returnRotation = 10;
    public float lookingTime;

    public ParticleSystem waitParticle;
    public ParticleSystem trailParticle;

    int _sequence;
    float _currentLookingTime;
    float _currentBashWaitTime;
    Vector3 _returnPosition;
    Vector3 _originalRotation;

    public override void ExecuteState()
    {       
        _sequence = 0;

        _currentLookingTime = lookingTime;
        _currentBashWaitTime = bashWaitTime;
        _returnPosition = new Vector3(0, transform.parent.position.y, transform.parent.position.z);
        _originalRotation = transform.parent.eulerAngles;
        waitParticle.Play();
    }

    public override void UpdateState()
    {
        if (AnimationStartEnded())
        {
            if (_sequence == 0)
            {
                LookAtPlayer();
            }
            else if(_sequence == 1)
            {
                Bash();
            }
            else if(_sequence == 2)
            {
                ReturnToLocation();
            }
            else if (_sequence == 3)
            {
                ResetRotation();
            }
        }
    }
    
    void LookAtPlayer()
    {
        //look at player smoothly
        Vector3 direction = player.transform.position - transform.parent.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);

        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, toRotation, 7 * Time.deltaTime);

        _currentLookingTime -= Time.deltaTime;

        if(_currentLookingTime <= 0)
        {
            _sequence++;
            animator.SetBool(animationName, true);
            waitParticle.Stop();
            trailParticle.Play();
        }
    }

    void Bash()
    {
        if(transform.parent.position.x <= -bashLimitX || transform.parent.position.x >= bashLimitX ||
           transform.parent.position.z <= -bashLimitZ || transform.parent.position.z >= bashLimitZ)
        {
            _currentBashWaitTime -= Time.deltaTime;
            animator.SetBool(animationName, false);
        }
        else
        {
            transform.parent.position += transform.forward * bashSpeed * Time.deltaTime;
        }

        if(_currentBashWaitTime <= 0)
        {
            _sequence++;
            trailParticle.Stop();
        }
    }

    void ReturnToLocation()
    {
        //look at player smoothly
        Vector3 direction = _returnPosition - transform.parent.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);

        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, toRotation, 10 * Time.deltaTime);


        transform.parent.position += transform.forward * returnSpeed * Time.deltaTime;
        

        if (Vector3.Distance(_returnPosition, transform.parent.position) <= 0.1f)
        {
            _sequence++;
        }
    }

    void ResetRotation()
    {
        if(transform.parent.eulerAngles.y < 180)
        {
            transform.parent.eulerAngles += transform.parent.up * returnRotation * Time.deltaTime;
        }
        else
        {
            transform.parent.eulerAngles -= transform.parent.up * returnRotation * Time.deltaTime;
        }

        if(Vector3.Distance(_originalRotation, transform.parent.eulerAngles) <= 1f * returnRotation)
        {
            transform.parent.eulerAngles = _originalRotation;

            FinishState();
        }
    }

    public override void FinishState()
    {
        EventHandler.instance.BossStateFinished();
    }
}
