using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIMenu;
    public GameObject UIGameplay;
    public GameObject UIEndgame;

    public TMP_Text highscoreTxt;
    public void GameplayStart()
    {
        UIMenu.SetActive(false);
        UIGameplay.SetActive(true);
    }

    public void GameplayEnds()
    {
        UIGameplay.SetActive(false);
        UIEndgame.SetActive(true);

        highscoreTxt.text = "High Score: " + GameManager.instance.highScore.ToString();
    }
}
