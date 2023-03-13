using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBomber : PowerUp
{
    public Shooting cannon;
    public override void Startup()
    {
        base.Startup();
        cannon.StartShooting();

    }

    public override void Deactivate()
    {
        cannon.StopShooting();
        base.Deactivate();
    }
}
