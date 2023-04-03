using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text highScoreTxt;
    public TMP_Text scrapsTxt;
    public TMP_Text perksTxt;

    public void Start()
    {
        UpdateTitle();
    }

    public void UpdateTitle()
    {
        highScoreTxt.text = "High Score: " + GameManager.instance.highScore.ToString();
        scrapsTxt.text = "Scraps: " + GameManager.instance.scrapAmount.ToString();
        perksTxt.text = "Cores: " + GameManager.instance.perksAmount.ToString();
    }
}
