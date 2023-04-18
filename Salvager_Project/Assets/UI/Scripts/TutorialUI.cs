using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TutorialUI : MonoBehaviour
{
    public GameState gameState;
    public Canvas UICanvas;
    public TMP_Text titleTxt;
    public TMP_Text infoTxt;

    private void Start()
    {
        if(GameManager.instance.firstTimeOpening)
        {
            UICanvas.gameObject.SetActive(true);
            titleTxt.text = "WELCOME";
            infoTxt.text = "Hello SALVAGER, look around and whenever you are ready, let's start your first mission";
            GameManager.instance.firstTimeOpening = false;
        }
    }

    public void CheckCustomize()
    {
        if (GameManager.instance.firstTimeCustomize)
        {
            UICanvas.gameObject.SetActive(true);
            titleTxt.text = "CUSTOMIZATION";
            infoTxt.text = "Here you can rebuild ships using the SCRAPS you gather in your missions. You can also use the SCRAPS to unlock new colors.";
            GameManager.instance.firstTimeCustomize = false;
        }
    }

    public void CheckPerks()
    {
        if (GameManager.instance.firstTimePerks)
        {
            UICanvas.gameObject.SetActive(true);
            titleTxt.text = "PERKS";
            infoTxt.text = "Here you will be able to unlock perks by spending PERK CORES. When equiped, the perks will help you in your missions.";
            GameManager.instance.firstTimePerks = false;
        }
    }

    public void CheckLog()
    {
        if (GameManager.instance.firstTimeLog)
        {
            UICanvas.gameObject.SetActive(true);
            titleTxt.text = "LOG";
            infoTxt.text = "In the log you will be able to see information of the creatures and items you encountered";
            GameManager.instance.firstTimeLog = false;
        }
    }

    public void CheckGameplay()
    {
        if (GameManager.instance.firstTimeGameplay)
        {
            gameState.PauseGame(0);
            UICanvas.gameObject.SetActive(true);
            titleTxt.text = "MISSION";
            infoTxt.text = "When exploring on a mission, make sure to defeat as many creatures you can to salvage the scraps of attacked ships. Be careful, as you go further, tougher creatures will appear.";
            GameManager.instance.firstTimeGameplay = false;
        }
    }

    public void RemoveTutorial()
    {
        UICanvas.gameObject.SetActive(false);

        if(Time.timeScale == 0)
        {
            gameState.PauseGame(1);
        }
    }
}
