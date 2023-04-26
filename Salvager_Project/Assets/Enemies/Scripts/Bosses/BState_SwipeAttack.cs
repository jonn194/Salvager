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
    Vector3 _originalPos;
    Vector3 _retreiveVector;
    public override void ExecuteState()
    {
        base.ExecuteState();

        _sequence = 0;

        weapon.gameObject.SetActive(true);
        
        float randomSide = Random.Range(0, 10);
        float randomVariation = Random.Range(0, ZVariation);
        float newX = spawnPosition.x;

        //set positions
        _originalPos = transform.parent.position;
        _retreiveVector = new Vector3(transform.parent.position.x, transform.parent.position.y, retreivePosition);


        if (randomSide < 5)
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
    }

    public override void UpdateState()
    {
        if(_sequence == 0)
        {
            HideBoss();
        }
        else if (_sequence == 1)
        {
            weapon.transform.Rotate(weapon.transform.up * rotationSpeed * _direction * Time.deltaTime);

            if (CheckRotation(weapon.transform.rotation, 180, 2.5f))
            {
                animator.SetBool(animationName, false);
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
        Vector3 temp = Vector3.Lerp(transform.parent.position, _retreiveVector, mainMovementSpeed * Time.deltaTime);
        transform.parent.position = temp;

        if (CheckDistance(transform.parent.position, _retreiveVector, 1f))
        {
            _sequence++;
        }
    }

    void RestoreBoss()
    {
        Vector3 temp = Vector3.Lerp(transform.parent.position, _originalPos, mainMovementSpeed * Time.deltaTime);
        transform.parent.position = temp;

        if (CheckDistance(transform.parent.position, _originalPos, 1f))
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
