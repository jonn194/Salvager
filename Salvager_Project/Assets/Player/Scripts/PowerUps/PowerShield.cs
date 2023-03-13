using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerShield : PowerUp
{
    public int maxHits;
    int _currentHits;

    public override void Startup()
    {
        base.Startup();
        _currentHits = maxHits;
    }

    public void GetHit()
    {
        effect.Play();
        _currentHits--;

        if ( _currentHits <= 0 )
        {
            Deactivate();
        }
    }
}
