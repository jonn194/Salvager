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

    public float movementSpeed;
    public float movementMaxX;

    float _direction = 1;
    bool _shooting;

    public override void ExecuteState()
    {
        base.ExecuteState();

        lasers[0].transform.position = new Vector3(originalX, lasers[0].transform.position.y, transform.parent.position.z - 3);
        lasers[0].StartLaser();

        lasers[1].transform.position = new Vector3(-originalX, lasers[1].transform.position.y, transform.parent.position.z - 3);
        lasers[1].StartLaser();

        _shooting = true;
        StartCoroutine(LaserTimer());
    }

    public override void UpdateState()
    {
        lasers[0].transform.position += lasers[0].transform.right * laserSpeed * Time.deltaTime;
        lasers[1].transform.position -= lasers[1].transform.right * laserSpeed * Time.deltaTime;

        MoveBoss();

        if (lasers[0].transform.position.x <= -originalX)
        {
            FinishState();
        }
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
        
        if (_shooting)
        {
            foreach(EnemyLaser l in lasers)
            {
                lasers[0].StopLaser();
                lasers[1].StopLaser();
            }
            
            _shooting = false;
        }
        
        base.FinishState();
    }
}
