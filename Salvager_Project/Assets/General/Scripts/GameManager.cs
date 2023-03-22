using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;

    [Header("Stats")]
    public int scrapAmount;
    public int perksAmount;
    public int highScore;
    public int currentScore;

    [Header("Ships")]
    public List<bool> shipsState = new List<bool>();
    public List<int> shipsPrices = new List<int>();
    public List<int> shipsSelectedColor = new List<int>();
    public int currentShip;

    public bool[,] availableColors = new bool[10,4];

    [Header("Perks")]
    public List<bool> perksState = new List<bool>();
    public List<int> perksPrices = new List<int>();
    public int currentPerk;

    [Header("Log Entries")]
    public List<bool> logItemsState = new List<bool>();
    public List<bool> logEnemiesState = new List<bool>();
    public List<bool> logBossesState = new List<bool>();


    [Header("Preferences")]
    public float musicVolume;
    public bool musicMuted;
    public float sfxVolume;
    public bool sfxMuted;

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
