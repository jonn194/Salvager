using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeta : Boss
{
    [Header("Boss Beta")]
    public BossState sSideMovement;
    public BState_SpawnShield sSpawnShields;
    public BossState sAlternatingLasers;
    public BossState sLaserSwipe;
    public BossState sSearchLaser;

    public EnemyShieldHandler originalShield;
    EnemyShieldHandler _spawnedShield;

    public override void Start()
    {
        _spawnedShield = Instantiate(originalShield, transform.parent);
        _spawnedShield.transform.position = new Vector3(0, 0, 10f);
        _spawnedShield.transform.rotation = transform.rotation;

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

    public override void SetStatesDependencies()
    {
        sEnterLevel.animator = animator;
        sDeath.animator = animator;
        sSideMovement.animator = animator;

        sSpawnShields.animator = animator;
        sAlternatingLasers.animator = animator;
        sLaserSwipe.animator = animator;
        sSearchLaser.animator = animator;

        sSearchLaser.player = player;
        sSpawnShields.shields = _spawnedShield;
    }
}
