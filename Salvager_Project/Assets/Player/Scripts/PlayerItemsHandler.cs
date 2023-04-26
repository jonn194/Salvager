using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsHandler : MonoBehaviour
{
    public ParticleSystem pickupParticles;
    
    [Header("Dependencies")]
    public PlayerStats playerStats;
    public PowerShield shield;
    public PowerTrishot trishot;
    public PowerLaser laser;
    public PowerBomber bomber;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickUpClip;
    public AudioClip healClip;

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
        if(shield.gameObject.activeSelf)
        {
            shield.Refill();
        }
        else
        {
            shield.gameObject.SetActive(true);
            shield.Startup();
            EventHandler.instance.PlayerPowerUp();
        }
    }

    public void ModuleTrishot()
    {
        if (trishot.gameObject.activeSelf)
        {
            trishot.Refill();
        }
        else
        {
            trishot.gameObject.SetActive(true);
            trishot.Startup();
            EventHandler.instance.PlayerPowerUp();
        }

    }

    public void ModuleLaser()
    {
        if (laser.gameObject.activeSelf)
        {
            laser.Refill();
        }
        else
        {
            laser.gameObject.SetActive(true);
            laser.Startup();
            EventHandler.instance.PlayerPowerUp();
        }
    }

    public void ModuleBomber()
    {
        if (bomber.gameObject.activeSelf)
        {
            bomber.Refill();
        }
        else
        {
            bomber.gameObject.SetActive(true);
            bomber.Startup();
            EventHandler.instance.PlayerPowerUp();
        }

    }

    public void PlayEffects(bool isHeal)
    {
        pickupParticles.Play();
        PlayAudio(isHeal);
    }

    void PlayAudio(bool isHeal)
    {
        if (audioSource)
        {
            if(isHeal)
            {
                audioSource.clip = healClip;
            }
            else
            {
                audioSource.clip = pickUpClip;
            }
              
            audioSource.Play();
        }
    }

    public void DeactivateAll()
    {
        shield.Deactivate();
        trishot.Deactivate();
        /*foreach(Shooting s in trishot.cannons)
        {
            s.bulletPool.ClearAll();
        }*/
        laser.Deactivate();
        bomber.Deactivate();
        //bomber.cannon.bulletPool.ClearAll();
    }
}
