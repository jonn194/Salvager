using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeta : Boss
{
    [Header("Boss Beta")]
    public BossState sSideMovement;
    public BossState sSpawnShields;
    public BossState sLaserSwipe;
    public BossState sSearchLaser;

    public override void SetStatesConnections()
    {
        sSearchLaser.player = player;

        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sSpawnShields, sLaserSwipe };

            sSpawnShields.possibleConnections = new List<BossState>() { sSideMovement, sLaserSwipe };

            sLaserSwipe.possibleConnections = new List<BossState>() { sSideMovement, sSpawnShields };

            sSearchLaser.possibleConnections = new List<BossState>() { sSideMovement, sSpawnShields, sLaserSwipe };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sSearchLaser);
            sSpawnShields.possibleConnections.Add(sSearchLaser);
            sLaserSwipe.possibleConnections.Add(sSearchLaser);
        }
    }
}
