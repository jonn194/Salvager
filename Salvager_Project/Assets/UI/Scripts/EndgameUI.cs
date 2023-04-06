using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndgameUI : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text scraps;
    public TMP_Text perks;

    public float speed;

    public Button retryButton;
    
    int _currentScraps;
    bool _scrapsDone;
    int _currentPerks;
    bool _perksDone;

    public void Setup()
    {
        _currentScraps = 0;
        _scrapsDone = false;
        _currentPerks = 0;
        _perksDone = false;

        retryButton.interactable = false;
    }

    private void Update()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        score.text = "Score: " + GameManager.instance.currentScore;


        if(_currentScraps >= GameManager.instance.currentScraps)
        {
            _currentScraps = GameManager.instance.currentScraps;
            _scrapsDone = true;
        }
        else
        {
            _currentScraps += Mathf.RoundToInt(speed * Time.deltaTime);
        }

        if (_currentPerks >= GameManager.instance.currentPerkCores)
        {
            _currentPerks = GameManager.instance.currentPerkCores;
            _perksDone = true;
        }
        else
        {
            _currentPerks += Mathf.RoundToInt(speed * Time.deltaTime);
        }


        scraps.text = "Scraps: " + _currentScraps;
        perks.text = "Perk Cores: " + _currentPerks;

        if(_scrapsDone && _perksDone)
        {
            retryButton.interactable = true;
        }
    }
}
