using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BState_LaserSwipe : BossState
{
    [Header("Laser Swipe State")]
    public EnemyLaser laser;

    public float laserDurationOn;
    public float laserDurationOff;

    public float laserSpeed;

    public float originalX;

    float _direction;

    public override void ExecuteState()
    {
        laser.transform.position = new Vector3(originalX, laser.transform.position.y, laser.transform.position.z);
    }

    public override void UpdateState()
    {
        laser.transform.position += laser.transform.right * laserSpeed * Time.deltaTime;


        if(Mathf.Abs(-originalX - laser.transform.position.x) <= 0.5f)
        {
            FinishState();
        }
    }

    public override void FinishState()
    {
        base.FinishState();
    }
}
