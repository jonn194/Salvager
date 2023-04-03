using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SwipeAttack : BossState
{
    [Header("Swipe Attack State")]
    public Vector3 spawnPosition;
    public float rotationSpeed;
    public BossDeltaWeapon weapon;

    float _direction = 1;

    public override void ExecuteState()
    {
        base.ExecuteState();

        weapon.gameObject.SetActive(true);
        
        float random = Random.Range(0, 10);
        float newX = spawnPosition.x;

        if(random < 5)
        {
            newX = -spawnPosition.x;
        }
        
        weapon.transform.position = new Vector3 (newX, spawnPosition.y, spawnPosition.z);


        if (newX <= 0)
        {
            _direction = -1;
            weapon.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            _direction = 1;
            weapon.transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }

    public override void UpdateState()
    {
        weapon.transform.eulerAngles += weapon.transform.up * rotationSpeed * _direction * Time.deltaTime;

        if(Mathf.Abs(180 - weapon.transform.eulerAngles.y) <= 1f)
        {
            FinishState();
        }
    }

    public override void FinishState()
    {
        weapon.gameObject.SetActive(false);
        base.FinishState();
    }
}
