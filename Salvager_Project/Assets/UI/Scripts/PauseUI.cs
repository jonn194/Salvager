using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUI : MonoBehaviour
{
    public TMP_Text scrapsTxt;
    public TMP_Text perksTxt;

    public void UpdateUI()
    {
        scrapsTxt.text = "Scraps: " + GameManager.instance.currentScraps;
        perksTxt.text = "Perk Cores: " + GameManager.instance.currentPerkCores;
    }
}
