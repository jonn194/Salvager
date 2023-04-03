using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Texts")]
    public TMP_Text currentScoreTxt;
    public TMP_Text currentScrapsTxt;
    public TMP_Text currentPerksTxt;

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
        UpdateScore();
        UpdateScrapsAndPerks();
    }

    void UpdatePowerUp()
    {
            shield.fillAmount = (float)((items.shield.currentLifetime * 100f) / items.shield.maxLifetime) / 100f;
            trishot.fillAmount = (float)((items.trishot.currentLifetime * 100f) / items.trishot.maxLifetime) / 100f;
            laser.fillAmount = (float)((items.laser.currentLifetime * 100f) / items.laser.maxLifetime) / 100f;
            bomber.fillAmount = (float)((items.bomber.currentLifetime * 100f) / items.bomber.maxLifetime) / 100f;
    }

    void UpdateScore()
    {
        currentScoreTxt.text = "Score: " + GameManager.instance.currentScore.ToString();
    }

    void UpdateScrapsAndPerks()
    {
        currentScrapsTxt.text = "Scraps: " + GameManager.instance.currentScraps.ToString();
        currentPerksTxt.text = "Cores: " + GameManager.instance.currentPerkCores.ToString();
    }
}
