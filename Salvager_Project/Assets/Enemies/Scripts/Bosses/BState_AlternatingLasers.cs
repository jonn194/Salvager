using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BState_AlternatingLasers : BossState
{
    [Header("Alternating Laser State")]
    public List<EnemyLaser> set01 = new List<EnemyLaser>();
    public List<EnemyLaser> set02 = new List<EnemyLaser>();
    public List<float> set01Positions = new List<float>();
    public List<float> set02Positions = new List<float>();
    public float setDuration;

    public float movementSpeed;
    public float movementMaxX;

    float _direction = 1;
    int _shootingCount = 0;

    public override void ExecuteState()
    {
        base.ExecuteState();

        for (int i = 0; i < set01.Count; i++)
        {
            set01[i].transform.position = new Vector3(set01Positions[i], set01[i].transform.position.y, transform.parent.position.z - 3);
        }

        for (int i = 0; i < set02.Count; i++)
        {
            set02[i].transform.position = new Vector3(set02Positions[i], set02[i].transform.position.y, transform.parent.position.z - 3);
        }

        _shootingCount = 0;
        foreach(EnemyLaser l in set01)
        {
            l.StartLaser();
        }
        StartCoroutine(SetTimer());
    }

    public override void UpdateState()
    {
        MoveBoss();
    }

    void MoveBoss()
    {
        transform.parent.position += transform.parent.right * movementSpeed * _direction * Time.deltaTime;

        if (transform.parent.position.x <= -movementMaxX)
        {
            _direction = -1;
        }
        else if (transform.parent.position.x >= movementMaxX)
        {
            _direction = 1;
        }
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
        else if(_shootingCount >= 4)
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
