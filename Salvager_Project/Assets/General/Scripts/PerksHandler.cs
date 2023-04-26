using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Button itemButton;
    public PlayerItemsHandler playerItems;

    [Header("Player Magnet")]
    public PerkPowerMagnet magnet;

    [Header("Player Spikes")]
    public PerkPowerSpikes spikes;

    int baseHP;

    public enum PerkTypes { None, ExtraLife, DoubleDamage, PiercingBullets, Magnet, RandomItem, Spikes}

    private void Start()
    {
        //getPresets
        baseHP = playerStats.maxHP;
    }

    public void StartPerks()
    {
        int currentPerk = GameManager.instance.currentPerk;


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
            playerStats.maxHP += 2;
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
            itemButton.gameObject.SetActive(true);
        }
        else
        {
            itemButton.gameObject.SetActive(false);
        }
    }

    public void ActivateRandomItem()
    {
        int randomItem = Random.Range(0, 5);

        switch (randomItem)
        {
            case 0:
                playerItems.EnergyCore();
                break;
            case 1:
                playerItems.Shield();
                break;
            case 2:
                playerItems.ModuleTrishot();
                break;
            case 3:
                playerItems.ModuleLaser();
                break;
            case 4:
                playerItems.ModuleBomber();
                break;
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

    public void DeactivateAll()
    {
        playerStats.maxHP = baseHP;
        playerBullets.bulletPrefab = basicBullet;
        magnet.gameObject.SetActive(false);
        itemButton.gameObject.SetActive(false);
        spikes.gameObject.SetActive(false);

    }
}
