using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;
    public bool gameStarted;

    [Header("Stats")]
    public int highScore;
    public int scrapAmount;
    public int perksAmount;

    public int currentScore = 0;
    public int currentScraps = 0;
    public int currentPerkCores = 0;


    [Header("Ships")]
    public List<bool> shipsState = new List<bool>();
    public List<int> shipsPrices = new List<int>();
    public List<int> shipsSelectedColor = new List<int>();
    public int currentShip;

    [Header("Colors")]
    public int basicColorPrice = 10;
    public int specialColorPrice = 30;
    public bool[,] availableColors = new bool[10,8];
    public int currentColor;

    [Header("Perks")]
    public List<bool> perksState = new List<bool>();
    public List<int> perksPrices = new List<int>();
    public int currentPerk;

    [Header("Log Entries")]
    public List<bool> logItemsState = new List<bool>();
    public List<bool> logEnemiesState = new List<bool>();
    public List<bool> logBossesState = new List<bool>();


    [Header("Preferences")]
    public bool masterActive = true;
    public float masterVolume;
    public bool musicActive = true;
    public float musicVolume;
    public bool sfxActive = true;
    public float sfxVolume;
    public bool postProcessActive = true;
    public bool rumbleActive = true;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < 10; i++)
        {
            availableColors[i, 0] = true;
            availableColors[i, 4] = true;
        }

        availableColors[2, 2] = true;
        availableColors[3, 3] = true;
        availableColors[5, 1] = true;
        availableColors[5, 2] = true;
        availableColors[9, 3] = true;
    }

    void SaveGame()
    {
        SaveData data = new SaveData(); //assign proper information
        string jsonData = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("SvgrSv", jsonData);
    }

    void LoadGame()
    {
        string data = PlayerPrefs.GetString("SvgrSv");
        SaveData loadedData = JsonUtility.FromJson<SaveData>(data);
    }

    void EraseGame()
    {
        PlayerPrefs.DeleteKey("SvgrSv");
    }
}
