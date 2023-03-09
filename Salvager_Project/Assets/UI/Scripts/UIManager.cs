using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject UIMenu;
    public GameObject UICustomize;
    public GameObject UIGameplay;
    public GameObject UIEndgame;

    public TMP_Text highscoreTxt;
    public TMP_Text currentScoreTxt;

    bool _gameplayActive;

    private void Update()
    {
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        if(_gameplayActive)
        {
            currentScoreTxt.text = "Score: " + GameManager.instance.currentScore.ToString();
        }
    }

    public void GameplayStart()
    {
        UIMenu.SetActive(false);
        UIGameplay.SetActive(true);
        _gameplayActive = true;
    }

    public void GameplayEnds()
    {
        UIGameplay.SetActive(false);
        UIEndgame.SetActive(true);

        highscoreTxt.text = "High Score: " + GameManager.instance.highScore.ToString();

        _gameplayActive = false;
    }

}
