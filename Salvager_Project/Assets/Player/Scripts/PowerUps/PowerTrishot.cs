using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTrishot : PowerUp
{
    public List<Shooting> cannons = new List<Shooting>();


    public override void Startup()
    {
        base.Startup();
        foreach (Shooting s in cannons)
        {
            s.StartShooting();
        }
    }

    public override void Deactivate()
    {
        foreach (Shooting s in cannons)
        {
            s.StopShooting();
        }

        base.Deactivate();
    }
}
