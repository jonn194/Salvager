using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [Header("Life")]
    public PlayerStats stats;
    public Image lifeBar;

    [Header("Power Ups")]
    public PlayerItemsHandler items;

    [Header("Icons")]
    public Image shield;
    public Image trishot;
    public Image laser;
    public Image bomber;


    private void Start()
    {
        EventHandler.instance.playerHPChanged += UpdateLifebar;
    }

    void UpdateLifebar()
    {
        lifeBar.fillAmount = (float)((stats.currentHP * 100f) / stats.maxHP) / 100f;
    }

    private void Update()
    {
        UpdatePowerUp();
    }

    void UpdatePowerUp()
    {
            shield.fillAmount = (float)((items.shield.currentLifetime * 100f) / items.shield.maxLifetime) / 100f;
            trishot.fillAmount = (float)((items.trishot.currentLifetime * 100f) / items.trishot.maxLifetime) / 100f;
            laser.fillAmount = (float)((items.laser.currentLifetime * 100f) / items.laser.maxLifetime) / 100f;
            bomber.fillAmount = (float)((items.bomber.currentLifetime * 100f) / items.bomber.maxLifetime) / 100f;
    }
}
