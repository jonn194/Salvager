using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerShield : PowerUp
{
    public int maxHits;
    int _currentHits;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shieldHitClip;

    public override void Startup()
    {
        base.Startup();
        _currentHits = maxHits;
    }

    public void GetHit()
    {
        effect.Play();
        _currentHits--;

        if(audioSource)
        {
            audioSource.clip = shieldHitClip;
            audioSource.Play();
        }

        if ( _currentHits <= 0 )
        {
            Deactivate();
        }
    }
}
