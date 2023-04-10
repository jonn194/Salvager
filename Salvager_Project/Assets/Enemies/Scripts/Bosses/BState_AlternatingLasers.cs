using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_AlternatingLasers : BossState
{
    [Header("Alternating Laser State")]
    public List<EnemyLaser> set01 = new List<EnemyLaser>();
    public List<EnemyLaser> set02 = new List<EnemyLaser>();

    public float setDuration;
    int _shootingCount = 0;

    public override void ExecuteState()
    {
        base.ExecuteState();

        _shootingCount = 0;
        foreach(EnemyLaser l in set01)
        {
            l.StartLaser();
        }
        StartCoroutine(SetTimer());
    }

    public override void UpdateState()
    {
    }

    IEnumerator SetTimer()
    {
        yield return new WaitForSeconds(setDuration);
        ChangeSet();
    }

    void ChangeSet()
    {
        _shootingCount++;

        if(_shootingCount == 1 || _shootingCount == 3)
        {
            foreach (EnemyLaser l in set01)
            {
                l.StopLaser();
            }
            foreach (EnemyLaser l in set02)
            {
                l.StartLaser();
            }

            StartCoroutine(SetTimer());
        }
        else if(_shootingCount == 0 || _shootingCount == 2)
        {
            foreach (EnemyLaser l in set01)
            {
                l.StartLaser();
            }
            foreach (EnemyLaser l in set02)
            {
                l.StopLaser();
            }

            StartCoroutine(SetTimer());
        }
        else if(_shootingCount == 4)
        {
            FinishState();
        }
    }

    public override void FinishState()
    {
        foreach (EnemyLaser l in set01)
        {
            l.StopLaser();
        }
        foreach (EnemyLaser l in set02)
        {
            l.StopLaser();
        }
        StopAllCoroutines();
        base.FinishState();
    }
}
