using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class BossAlpha : Boss
{
    [Header("Boss Alpha")]
    public BossState sSideMovement;
    public BossState sCanonAttack;
    public BossState sMultipleAttack;
    public BossState sHeadBashAttack;

    public override void SetStatesConnections()
    {
        sHeadBashAttack.player = player;

        if(currentLife > maxLife/2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sCanonAttack, sMultipleAttack };

            sCanonAttack.possibleConnections = new List<BossState>() { sSideMovement, sMultipleAttack };

            sMultipleAttack.possibleConnections = new List<BossState>() { sSideMovement, sCanonAttack };

            sHeadBashAttack.possibleConnections = new List<BossState>() { sSideMovement, sCanonAttack, sMultipleAttack };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sHeadBashAttack);
            sCanonAttack.possibleConnections.Add(sHeadBashAttack);
            sMultipleAttack.possibleConnections.Add(sHeadBashAttack);
        }
    }
}
