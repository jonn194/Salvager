using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksHandler : MonoBehaviour
{
    public PlayerStats playerStats;
    public BulletPool playerBullets;

    public Bullet basicBullet;
    public Bullet doubleBullet;
    public Bullet piercingBullet;

    int baseHP;

    public enum PerkTypes { ExtraLife, DoubleDamage, PiercingBullets, Magnet, RandomItem, Spikes}

    public void StartPerks()
    {
        int currentPerk = GameManager.instance.currentPerk;

        //getPresets
        baseHP = playerStats.maxHP;

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

        if(currentPerk == (int)PerkTypes.ExtraLife)
        {
            playerStats.maxHP += playerStats.maxHP / 2;
            playerStats.currentHP = playerStats.maxHP;
        }
        else
        {
            playerStats.maxHP += baseHP;
            playerStats.currentHP = playerStats.maxHP;
        }
    }
}
