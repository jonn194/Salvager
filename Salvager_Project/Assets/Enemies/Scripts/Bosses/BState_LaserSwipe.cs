using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BState_LaserSwipe : BossState
{
    [Header("Laser Swipe State")]
    public List<EnemyLaser> lasers = new List<EnemyLaser>();

    public float laserDuration;

    public float laserSpeed;

    public float originalX;

    bool _shooting;

    public override void ExecuteState()
    {
        lasers[0].transform.position = new Vector3(originalX, lasers[0].transform.position.y, lasers[0].transform.position.z);
        lasers[0].StartLaser();

        lasers[1].transform.position = new Vector3(-originalX, lasers[1].transform.position.y, lasers[1].transform.position.z);
        lasers[1].StartLaser();

        _shooting = true;
        StartCoroutine(LaserTimer());
    }

    public override void UpdateState()
    {
        lasers[0].transform.position += lasers[0].transform.right * laserSpeed * Time.deltaTime;
        lasers[1].transform.position -= lasers[1].transform.right * laserSpeed * Time.deltaTime;


        if (Mathf.Abs(-originalX - lasers[0].transform.position.x) <= 0.5f)
        {
            FinishState();
        }
    }

    IEnumerator LaserTimer()
    {
        yield return new WaitForSeconds(laserDuration + lasers[0].warningDuration);
        ToggleLaser();
    }

    void ToggleLaser()
    {
        if(_shooting)
        {
            lasers[0].StopLaser();
            lasers[1].StopLaser();
            _shooting = false;
        }
        else
        {
            lasers[0].StartLaser();
            lasers[1].StartLaser();
            _shooting = true;
        }

        StartCoroutine(LaserTimer());
    }


    public override void FinishState()
    {
        StopAllCoroutines();
        base.FinishState();
    }
}
