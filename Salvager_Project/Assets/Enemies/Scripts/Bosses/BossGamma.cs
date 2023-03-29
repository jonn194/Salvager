using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGamma : Boss
{
    [Header("Boss Gamma")]
    public BossState sSideMovement;
    public BossState sMoveAround;
    public BossState sAlignProtectors;
    public BossState sShootProtectors;
    public BossState sExpandProtectors;


    public override void SetStatesConnections()
    {
        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sMoveAround, sAlignProtectors, sShootProtectors };

            sMoveAround.possibleConnections = new List<BossState>() { sSideMovement, sAlignProtectors, sShootProtectors };

            sAlignProtectors.possibleConnections = new List<BossState>() { sSideMovement, sMoveAround, sShootProtectors };

            sShootProtectors.possibleConnections = new List<BossState>() { sSideMovement, sMoveAround, sAlignProtectors };

            sExpandProtectors.possibleConnections = new List<BossState>() { sSideMovement, sMoveAround, sAlignProtectors, sShootProtectors };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sExpandProtectors);
            sMoveAround.possibleConnections.Add(sExpandProtectors);
            sAlignProtectors.possibleConnections.Add(sExpandProtectors);
            sShootProtectors.possibleConnections.Add(sExpandProtectors);
        }
    }
}
