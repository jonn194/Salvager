using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class BossAlpha : Boss
{
    [Header("Boss Alpha")]
    public BossState sSideMovement;
    public BossState sCanonAttack;
    public BossState sBombsAttack;
    public BossState sMultipleAttack;
    public BossState sHeadBashAttack;

    public List<BulletPool> bulletPools = new List<BulletPool>();

    public override void SetStatesConnections()
    {
        sHeadBashAttack.player = player;

        if(currentLife > maxLife/2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sCanonAttack, sMultipleAttack, sBombsAttack };

            sCanonAttack.possibleConnections = new List<BossState>() { sSideMovement, sMultipleAttack, sBombsAttack };

            sBombsAttack.possibleConnections = new List<BossState>() { sSideMovement, sCanonAttack, sMultipleAttack };

            sMultipleAttack.possibleConnections = new List<BossState>() { sSideMovement, sCanonAttack, sBombsAttack };

            sHeadBashAttack.possibleConnections = new List<BossState>() { sSideMovement, sCanonAttack, sBombsAttack, sMultipleAttack };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sHeadBashAttack);
            sCanonAttack.possibleConnections.Add(sHeadBashAttack);
            sBombsAttack.possibleConnections.Add(sHeadBashAttack);
            sMultipleAttack.possibleConnections.Add(sHeadBashAttack);
        }
    }

    public override void DestroyBoss()
    {
        foreach(BulletPool b in bulletPools)
        {
            b.ClearAll();
        }

        base.DestroyBoss();
    }
}
