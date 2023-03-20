using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsHandler : MonoBehaviour
{
    public ParticleSystem pickupParticles;
    
    [Header("Dependencies")]
    public PlayerStats playerStats;
    public PowerUp shield;
    public PowerUp trishot;
    public PowerUp laser;
    public PowerUp bomber;

    public void EnergyCore()
    {
        playerStats.currentHP++;

        if(playerStats.currentHP > playerStats.maxHP)
        {
            playerStats.currentHP = playerStats.maxHP;
        }
    }

    public void Shield()
    {
        shield.gameObject.SetActive(true);
        shield.Startup();
    }

    public void ModuleTrishot()
    {
        trishot.gameObject.SetActive(true);
        trishot.Startup();
    }

    public void ModuleLaser()
    {
        laser.gameObject.SetActive(true);
        laser.Startup();
    }

    public void ModuleBomber()
    {
        bomber.gameObject.SetActive(true);
        bomber.Startup();
    }

    public void PlayEffects()
    {
        pickupParticles.Play();
    }

    public void DeactivateAll()
    {
        shield.Deactivate();
        trishot.Deactivate();
        laser.Deactivate();
        bomber.Deactivate();
    }
}
