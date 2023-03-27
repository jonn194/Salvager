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
        sEnterLevel.possibleConnections = new BossState[1] { sSideMovement };

        sSideMovement.possibleConnections = new BossState[2] { sCanonAttack, sMultipleAttack };
        
        sCanonAttack.possibleConnections = new BossState[2] { sSideMovement, sMultipleAttack };

        sMultipleAttack.possibleConnections = new BossState[2] { sSideMovement, sCanonAttack };

        sHeadBashAttack.possibleConnections = new BossState[3] { sSideMovement, sCanonAttack, sMultipleAttack };
    }
}
