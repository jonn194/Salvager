using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeta : Boss
{
    [Header("Boss Beta")]
    public BossState sSideMovement;
    public BossState sSpawnShields;
    public BossState sAlternatingLasers;
    public BossState sLaserSwipe;
    public BossState sSearchLaser;

    public override void SetStatesConnections()
    {
        sSearchLaser.player = player;

        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

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
}
