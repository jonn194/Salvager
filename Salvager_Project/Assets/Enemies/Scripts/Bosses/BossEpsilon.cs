using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEpsilon : Boss
{
    [Header("Boss Epsilon")]
    public BossState sSideMovement;
    public BossState sBombsAttack;
    public BState_AlternatingLasers sAlternatingLasers;
    public BossState sMoveAround;
    public BState_TailAttack sTailAttack;

    public BulletPool bombsPools;
    public EnemyLaser originalLaser;
    public BossDeltaWeapon originalWeapon;
    List<EnemyLaser> _laserSet01 = new List<EnemyLaser>();
    List<EnemyLaser> _laserSet02 = new List<EnemyLaser>();
    BossDeltaWeapon _spawnedWeapon;


    public override void Start()
    {
        CreateWeapons();

        base.Start();
    }

    public override void SetStatesConnections()
    {
        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sBombsAttack, sAlternatingLasers, sMoveAround };

            sBombsAttack.possibleConnections = new List<BossState>() { sSideMovement, sAlternatingLasers, sMoveAround };

            sAlternatingLasers.possibleConnections = new List<BossState>() { sSideMovement, sBombsAttack};

            sMoveAround.possibleConnections = new List<BossState>() { sSideMovement, sBombsAttack, sAlternatingLasers };

            sTailAttack.possibleConnections = new List<BossState>() { sSideMovement, sBombsAttack, sAlternatingLasers, sMoveAround };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sTailAttack);
            sBombsAttack.possibleConnections.Add(sTailAttack);
            sAlternatingLasers.possibleConnections.Add(sTailAttack);
            sTailAttack.possibleConnections.Add(sTailAttack);
        }
    }

    void CreateWeapons()
    {
        //tail
        _spawnedWeapon = Instantiate(originalWeapon, transform.parent);
        _spawnedWeapon.transform.position = transform.position;
        _spawnedWeapon.transform.rotation = transform.rotation;
        _spawnedWeapon.gameObject.SetActive(false);
        _spawnedWeapon.mainBoss = this;

        //alternating lasers
        for (int i = 0; i < 2; i++)
        {
            EnemyLaser newLaser = Instantiate(originalLaser, transform.parent);
            newLaser.transform.position = transform.position;
            newLaser.transform.rotation = transform.rotation;
            _laserSet01.Add(newLaser);
        }

        for (int i = 0; i < 3; i++)
        {
            EnemyLaser newLaser = Instantiate(originalLaser, transform.parent);
            newLaser.transform.position = transform.position;
            newLaser.transform.rotation = transform.rotation;
            _laserSet02.Add(newLaser);
        }
    }

    public override void SetStatesDependencies()
    {
        sEnterLevel.animator = animator;
        sDeath.animator = animator;
        sSideMovement.animator = animator;

        sBombsAttack.animator = animator;
        sAlternatingLasers.animator = animator;
        sMoveAround.animator = animator;
        sTailAttack.animator = animator;

        sSideMovement.screenBoundaries = screenBoundaries;
        sBombsAttack.screenBoundaries = screenBoundaries;
        sMoveAround.screenBoundaries = screenBoundaries;
        sTailAttack.screenBoundaries = screenBoundaries;

        sTailAttack.weapon = _spawnedWeapon;
        sAlternatingLasers.set01 = _laserSet01;
        sAlternatingLasers.set02 = _laserSet02;

        sDeath.bossRef = this;
    }

    public override void CheckLife()
    {
        if (currentLife <= 0)
        {
            bombsPools.ClearAll();

            foreach (EnemyLaser l in _laserSet01)
            {
                l.StopLaser();
            }
            foreach (EnemyLaser l in _laserSet02)
            {
                l.StopLaser();
            }

            _spawnedWeapon.destroyed = true;
        }

        base.CheckLife();
    }

    public override void DestroyBoss()
    {
        GameManager.instance.logBossesState[4] = true;

        foreach (EnemyLaser l in _laserSet01)
        {
            Destroy(l.gameObject);
        }

        foreach (EnemyLaser l in _laserSet02)
        {
            Destroy(l.gameObject);
        }

        _laserSet01.Clear();
        _laserSet02.Clear();

        base.DestroyBoss();
    }
}
