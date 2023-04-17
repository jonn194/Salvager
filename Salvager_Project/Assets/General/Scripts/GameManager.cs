using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;
    public bool gameStarted;
    public bool gamePaused;

    [Header("Stats")]
    public int maxDificulty;
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
    public List<bool> availableColors = new List<bool>();

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

        for (int i = 0; i < shipsPrices.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(j == 0)
                {
                    availableColors.Add(true);
                }
                else
                {
                    availableColors.Add(false);
                }
            }
        }

        LoadGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            EraseGame();
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.maxDificulty = maxDificulty;
        data.highScore = highScore;
        data.scrapsAmount = scrapAmount;
        data.perksAmount = perksAmount;

        data.shipsState = shipsState;
        data.shipsSelectedColor = shipsSelectedColor;
        data.currentShip = currentShip;
        data.availableColors = availableColors;

        data.perksState = perksState;
        data.currentPerk = currentPerk;

        data.logItemsState = logItemsState;
        data.logEnemiesState = logEnemiesState;
        data.logBossesState = logBossesState;

        data.masterActive = masterActive;
        data.masterVolume = masterVolume;
        data.musicActive = musicActive;
        data.musicVolume = musicVolume;
        data.sfxActive = sfxActive;
        data.sfxVolume = sfxVolume;
        data.postProcessActive = postProcessActive;
        data.rumbleActive = rumbleActive;

        string jsonData = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("SvgrSv", jsonData);
    }

    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("SvgrSv"))
        {
            string stringData = PlayerPrefs.GetString("SvgrSv");
            Debug.Log(stringData);
            SaveData data = JsonUtility.FromJson<SaveData>(stringData);

            maxDificulty = data.maxDificulty;
            highScore = data.highScore;
            scrapAmount = data.scrapsAmount;
            perksAmount = data.perksAmount;

            shipsState = data.shipsState;
            shipsSelectedColor = data.shipsSelectedColor;
            currentShip = data.currentShip;

            if (data.availableColors.Count > 0)
            {
                availableColors = data.availableColors;
            }

            perksState = data.perksState;
            currentPerk = data.currentPerk;

            logItemsState = data.logItemsState;
            logEnemiesState = data.logEnemiesState;
            logBossesState = data.logBossesState;

            masterActive = data.masterActive;
            masterVolume = data.masterVolume;
            musicActive = data.musicActive;
            musicVolume = data.musicVolume;
            sfxActive = data.sfxActive;
            sfxVolume = data.sfxVolume;
            postProcessActive = data.postProcessActive;
            rumbleActive = data.rumbleActive;
        }
    }

    public void EraseGame()
    {
        PlayerPrefs.DeleteKey("SvgrSv");
    }
}
