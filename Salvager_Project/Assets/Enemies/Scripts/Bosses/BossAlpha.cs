using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAlpha : Boss
{
    [Header("Boss Alpha")]
    public BossState sSideMovement;
    public BState_CanonShooting sCanonAttack;
    public BState_CanonShooting sBombsAttack;
    public BState_MultipleShooting sMultipleAttack;
    public BossState sHeadBashAttack;

    public BulletPool bulletPool;
    public BulletPool canonPool;
    public BulletPool bombPool;

    public override void SetStatesConnections()
    {
        SetStatesDependencies();

        if (currentLife > maxLife/2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement, sCanonAttack };

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

    public override void SetStatesDependencies()
    {
        sEnterLevel.animator = animator;
        sDeath.animator = animator;
        sSideMovement.animator = animator;

        sCanonAttack.animator = animator;
        sBombsAttack.animator = animator;
        sMultipleAttack.animator = animator;
        sHeadBashAttack.animator = animator;

        sSideMovement.screenBoundaries = screenBoundaries;
        sCanonAttack.screenBoundaries = screenBoundaries;
        sBombsAttack.screenBoundaries= screenBoundaries;
        sHeadBashAttack.screenBoundaries = screenBoundaries;

        sDeath.bossRef = this;
        sHeadBashAttack.player = player;
    }

    public override void DestroyBoss()
    {
        GameManager.instance.logBossesState[0] = true;

        bulletPool.ClearAll();
        canonPool.ClearAll();
        bombPool.ClearAll();

        base.DestroyBoss();
    }
}
