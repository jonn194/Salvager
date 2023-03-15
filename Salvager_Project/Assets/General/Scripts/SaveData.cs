using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    //PLAYER DATA
    public int highScore;
    public int scrapsAmount;
    public int perksAmount;
    
    //SHIPS
    public List<bool> shipsState = new List<bool>();
    public List<int> shipsPrices = new List<int>();
    public List<int> shipsSelectedColor = new List<int>();
    public int currentShip;

    public bool[,] availableColors = new bool[10, 4];

    //PERKS
    public List<bool> perksState = new List<bool>();
    public int currentPerk;

    //LOG ENTRIES
    public List<bool> logItemsState = new List<bool>();
    public List<bool> logEnemiesState = new List<bool>();
    public List<bool> logBossesState = new List<bool>();

    //SETTINGS
    public float musicVolume;
    public bool musicMuted;
    public float sfxVolume;
    public bool sfxMuted;
}
