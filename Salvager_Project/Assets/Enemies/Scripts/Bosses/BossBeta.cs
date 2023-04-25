using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeta : Boss
{
    [Header("Boss Beta")]
    public BossState sSideMovement;
    public BState_SpawnShield sSpawnShields;
    public BState_AlternatingLasers sAlternatingLasers;
    public BState_LaserSwipe sLaserSwipe;
    public BState_LaserSearch sSearchLaser;

    public EnemyLaser originalLaser;
    List<EnemyLaser> _laserSet01 = new List<EnemyLaser>();
    List<EnemyLaser> _laserSet02 = new List<EnemyLaser>();
    List<EnemyLaser> _lasersSwipe = new List<EnemyLaser>();
    public EnemyLaser laserSearch;
    public EnemyShieldHandler originalShield;
    EnemyShieldHandler _spawnedShield;

    public override void Start()
    {
        CreateWeapons();

        base.Start();
    }

    public override void SetStatesConnections()
    {
        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSpawnShields };

            sSideMovement.possibleConnections = new List<BossState>() { sSpawnShields, sLaserSwipe, sAlternatingLasers };

            sSpawnShields.possibleConnections = new List<BossState>() { sSideMovement, sLaserSwipe, sAlternatingLasers };

            sAlternatingLasers.possibleConnections = new List<BossState>() { sSideMovement, sSpawnShields, sLaserSwipe };

            sLaserSwipe.possibleConnections = new List<BossState>() { sSideMovement, sSpawnShields, sAlternatingLasers };

            sSearchLaser.possibleConnections = new List<BossState>() { sSideMovement, sSpawnShields, sAlternatingLasers, sLaserSwipe };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sSearchLaser);
            sSpawnShields.possibleConnections.Add(sSearchLaser);
            sAlternatingLasers.possibleConnections.Add(sSearchLaser);
            sLaserSwipe.possibleConnections.Add(sSearchLaser);
        }
    }

    void CreateWeapons()
    {
        //shields
        _spawnedShield = Instantiate(originalShield, transform.parent);
        _spawnedShield.transform.position = new Vector3(0, 0, 10f);
        _spawnedShield.transform.rotation = transform.rotation;

        //swipe lasers
        for (int i = 0; i < 2; i++)
        {
            EnemyLaser newLaser = Instantiate(originalLaser, transform.parent);
            newLaser.transform.position = transform.position;
            newLaser.transform.rotation = transform.rotation;
            _lasersSwipe.Add(newLaser);
        }

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

        sSpawnShields.animator = animator;
        sAlternatingLasers.animator = animator;
        sLaserSwipe.animator = animator;
        sSearchLaser.animator = animator;

        sSideMovement.screenBoundaries = screenBoundaries;

        sAlternatingLasers.set01 = _laserSet01;
        sAlternatingLasers.set02 = _laserSet02;

        sLaserSwipe.lasers = _lasersSwipe;
        sSearchLaser.laser = laserSearch;

        sDeath.bossRef = this;
        sSearchLaser.player = player;
        sSpawnShields.shields = _spawnedShield;
    }

    public override void CheckLife()
    {
        if (currentLife <= 0)
        {
            _spawnedShield.shields[0].DestroyShield();
            _spawnedShield.shields[1].DestroyShield();

            foreach (EnemyLaser l in _laserSet01)
            {
                l.StopLaser();
            }
            foreach (EnemyLaser l in _laserSet02)
            {
                l.StopLaser();
            }
            foreach (EnemyLaser l in _lasersSwipe)
            {
                l.StopLaser();
            }
            laserSearch.StopLaser();
        }

        base.CheckLife();
    }

    public override void DestroyBoss()
    {
        GameManager.instance.logBossesState[1] = true;

        Destroy(_spawnedShield.gameObject);

        foreach (EnemyLaser l in _lasersSwipe)
        {
            Destroy(l.gameObject);
        }

        foreach (EnemyLaser l in _laserSet01)
        {
            Destroy(l.gameObject);
        }

        foreach (EnemyLaser l in _laserSet02)
        {
            Destroy(l.gameObject);
        }

        _lasersSwipe.Clear();
        _laserSet01.Clear();
        _laserSet02.Clear();

        base.DestroyBoss();
    }
}
