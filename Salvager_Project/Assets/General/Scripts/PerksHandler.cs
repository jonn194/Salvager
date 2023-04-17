using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksHandler : MonoBehaviour
{
    [Header("Player Stats")]
    public PlayerStats playerStats;

    [Header("Player Bullets")]
    public BulletPool playerBullets;

    public Bullet basicBullet;
    public Bullet doubleBullet;
    public Bullet piercingBullet;

    [Header("Player Items")]
    public PlayerItemsHandler playerItems;

    [Header("Player Magnet")]
    public PerkPowerMagnet magnet;

    [Header("Player Spikes")]
    public PerkPowerSpikes spikes;

    int baseHP;

    public enum PerkTypes { None, ExtraLife, DoubleDamage, PiercingBullets, Magnet, RandomItem, Spikes}


    public void StartPerks()
    {
        int currentPerk = GameManager.instance.currentPerk;

        //getPresets
        baseHP = playerStats.maxHP;

        CheckLife(currentPerk);
        CheckBullets(currentPerk);
        CheckMagnet(currentPerk);
        CheckRandomItem(currentPerk);
        CheckSpikes(currentPerk);
    }

    void CheckLife(int currentPerk)
    {
        if (currentPerk == (int)PerkTypes.ExtraLife)
        {
            playerStats.maxHP += playerStats.maxHP / 2;
        }
        else
        {
            playerStats.maxHP = baseHP;
        }
    }

    void CheckBullets(int currentPerk)
    {
        if (currentPerk == (int)PerkTypes.DoubleDamage)
        {
            playerBullets.bulletPrefab = doubleBullet;
        }
        else if (currentPerk == (int)PerkTypes.PiercingBullets)
        {
            playerBullets.bulletPrefab = piercingBullet;
        }
        else
        {
            playerBullets.bulletPrefab = basicBullet;
        }
    }

    void CheckMagnet(int currentPerk)
    {
        if (currentPerk == (int)PerkTypes.Magnet)
        {
            magnet.gameObject.SetActive(true);
            magnet.effect.Play();
        }
        else
        {
            magnet.gameObject.SetActive(false);
        }
    }

    void CheckRandomItem(int currentPerk)
    {
        if (currentPerk == (int)PerkTypes.RandomItem)
        {
            int randomItem = Random.Range(0, 4);

            switch(randomItem)
            {
                case 0:
                    playerItems.Shield();
                    break;
                case 1:
                    playerItems.ModuleTrishot();
                    break;
                case 2:
                    playerItems.ModuleLaser();
                    break;
                case 3:
                    playerItems.ModuleBomber();
                    break;
            }
        }
    }

    void CheckSpikes(int currentPerk)
    {
        if (currentPerk == (int)PerkTypes.Spikes)
        {
            spikes.gameObject.SetActive(true);
        }
        else
        {
            spikes.gameObject.SetActive(false);
        }
    }
}
