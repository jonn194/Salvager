using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SwipeAttack : BossState
{
    [Header("Swipe Attack State")]
    public Vector3 spawnPosition;
    public float rotationSpeed;
    public float ZVariation = 5;
    public BossDeltaWeapon weapon;

    public float retreivePosition = 25;
    public float mainMovementSpeed = 10;

    float _direction = 1;
    int _sequence;
    float _originalPos;
    public override void ExecuteState()
    {
        base.ExecuteState();

        _sequence = 0;

        weapon.gameObject.SetActive(true);
        
        float randomSide = Random.Range(0, 10);
        float randomVariation = Random.Range(0, ZVariation);
        float newX = spawnPosition.x;


        if(randomSide < 5)
        {
            newX = -spawnPosition.x;
        }
        
        //relocate weapon
        weapon.transform.position = new Vector3 (newX, spawnPosition.y, spawnPosition.z - randomVariation);

        if (newX <= 0)
        {
            _direction = -1;
            weapon.transform.eulerAngles = new Vector3(0, 45, 0);
        }
        else
        {
            _direction = 1;
            weapon.transform.eulerAngles = new Vector3(0, -45, 0);
        }
        
        //get original position in Z
        _originalPos = transform.parent.position.z;
    }

    public override void UpdateState()
    {
        if(_sequence == 0)
        {
            HideBoss();
        }
        else if (_sequence == 1)
        {
            weapon.transform.eulerAngles += weapon.transform.up * rotationSpeed * _direction * Time.deltaTime;

            if (Mathf.Abs(180 - weapon.transform.eulerAngles.y) <= 1f)
            {
                _sequence++;
            }
        }
        else if (_sequence == 2)
        {
            RestoreBoss();
        }
        else if (_sequence == 3)
        {
            FinishState();
        }
    }

    void HideBoss()
    {
        transform.parent.position -= transform.parent.forward * mainMovementSpeed * Time.deltaTime;

        if (Mathf.Abs(retreivePosition - transform.parent.position.z) <= 0.5f)
        {
            _sequence++;
        }
    }

    void RestoreBoss()
    {
        transform.parent.position += transform.parent.forward * mainMovementSpeed * Time.deltaTime;

        if (Mathf.Abs(_originalPos - transform.parent.position.z) <= 0.5f)
        {
            _sequence++;
        }
    }

    public override void FinishState()
    {
        weapon.gameObject.SetActive(false);
        base.FinishState();
    }
}
