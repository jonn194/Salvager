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
        playerStats.currentHP += 3;

        if(playerStats.currentHP > playerStats.maxHP)
        {
            playerStats.currentHP = playerStats.maxHP;
        }
        //Agregar evento de heal
        EventHandler.instance.HPChanged();
    }

    public void Shield()
    {
        shield.gameObject.SetActive(true);
        shield.Startup();
        EventHandler.instance.PlayerPowerUp();
    }

    public void ModuleTrishot()
    {
        trishot.gameObject.SetActive(true);
        trishot.Startup();
        EventHandler.instance.PlayerPowerUp();
    }

    public void ModuleLaser()
    {
        laser.gameObject.SetActive(true);
        laser.Startup();
        EventHandler.instance.PlayerPowerUp();
    }

    public void ModuleBomber()
    {
        bomber.gameObject.SetActive(true);
        bomber.Startup();
        EventHandler.instance.PlayerPowerUp();
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
