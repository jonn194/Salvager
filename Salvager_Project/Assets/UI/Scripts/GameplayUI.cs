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
    Image _shieldTimer;
    public Image trishot;
    Image _trishotTimer;
    public Image laser;
    Image _laserTimer;
    public Image bomber;
    Image _bomberTimer;

    [Header("Texts")]
    public TMP_Text currentScoreTxt;
    public TMP_Text currentScrapsTxt;
    public TMP_Text currentPerksTxt;

    [Header("Animations")]
    public Animator scrapsAnim;
    public Animator perksAnim;

    private void Start()
    {
        EventHandler.instance.playerHPChanged += UpdateLifebar;
        EventHandler.instance.scrapPickup += UpdateScrap;
        EventHandler.instance.perksPickup += UpdatePerk;


        _shieldTimer = shield.transform.GetChild(0).GetComponent<Image>();
        _trishotTimer = trishot.transform.GetChild(0).GetComponent<Image>();
        _laserTimer = laser.transform.GetChild(0).GetComponent<Image>();
        _bomberTimer = bomber.transform.GetChild(0).GetComponent<Image>();
    }

    void UpdateLifebar()
    {
        lifeBar.fillAmount = (float)((stats.currentHP * 100f) / stats.maxHP) / 100f;
    }

    private void Update()
    {
        UpdatePowerUp();
        UpdateScore();
    }

    void UpdatePowerUp()
    {
        if (items.shield.currentLifetime > 0)
        {
            shield.gameObject.SetActive(true);
            _shieldTimer.fillAmount = (float)((items.shield.currentLifetime * 100f) / items.shield.maxLifetime) / 100f;
        }
        else
        {
            shield.gameObject.SetActive(false);
        }

        if (items.trishot.currentLifetime > 0)
        {
            trishot.gameObject.SetActive(true);
            _trishotTimer.fillAmount = (float)((items.trishot.currentLifetime * 100f) / items.trishot.maxLifetime) / 100f;
        }
        else
        {
            trishot.gameObject.SetActive(false);
        }

        if (items.laser.currentLifetime > 0)
        {
            laser.gameObject.SetActive(true);
            _laserTimer.fillAmount = (float)((items.laser.currentLifetime * 100f) / items.laser.maxLifetime) / 100f;
        }
        else
        {
            laser.gameObject.SetActive(false);
        }

        if (items.bomber.currentLifetime > 0)
        {
            bomber.gameObject.SetActive(true);
            _bomberTimer.fillAmount = (float)((items.bomber.currentLifetime * 100f) / items.bomber.maxLifetime) / 100f;
        }
        else
        {
            bomber.gameObject.SetActive(false);
        }
    }

    void UpdateScore()
    {
        currentScoreTxt.text = "Score: " + GameManager.instance.currentScore.ToString();
    }

    void UpdateScrap()
    {
        currentScrapsTxt.text = "Scraps: " + GameManager.instance.currentScraps.ToString();
        scrapsAnim.SetTrigger("Animate");
    }

    void UpdatePerk()
    {
        currentPerksTxt.text = "Cores: " + GameManager.instance.currentPerkCores.ToString();
        perksAnim.SetTrigger("Animate");
    }
}
