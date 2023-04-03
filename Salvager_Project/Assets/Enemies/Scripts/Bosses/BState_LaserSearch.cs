using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_LaserSearch : BossState
{
    [Header("Laser Search State")]
    public EnemyLaser laser;
    public float laserTime;

    public float followSpeed;
    public float followTime;

    float _currentFollowTime;
    bool _shooted;

    public override void ExecuteState()
    {
        _currentFollowTime = followTime;
        _shooted = false;
    }

    public override void UpdateState()
    {
        _currentFollowTime -= Time.deltaTime;

        if(_currentFollowTime > 0)
        {
            float xPos = Mathf.Lerp(transform.parent.position.x, player.transform.position.x, followSpeed * Time.deltaTime);

            transform.parent.position = new Vector3(xPos, transform.parent.position.y, transform.parent.position.z);
        }
        else if(!_shooted)
        {
            _shooted = true;
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        laser.StartLaser();
        StartCoroutine(LaserTimer());
    }

    IEnumerator LaserTimer()
    {
        yield return new WaitForSeconds(followTime);
        laser.StopLaser();
        FinishState();
    }

    public override void FinishState()
    {
        StopAllCoroutines();

        laser.StopLaser();

        base.FinishState();
    }
}
