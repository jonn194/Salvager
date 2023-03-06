using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TMP_Text highscoreTxt;
    public TMP_Text currentScoreTxt;


    private void Update()
    {
        highscoreTxt.text = GameManager.instance.highScore.ToString();
        currentScoreTxt.text = GameManager.instance.currentScore.ToString();
    }
}
