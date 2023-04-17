using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    //PLAYER DATA
    public int maxDificulty;
    public int highScore;
    public int scrapsAmount;
    public int perksAmount;
    
    //SHIPS
    public List<bool> shipsState = new List<bool>();
    public List<int> shipsSelectedColor = new List<int>();
    public int currentShip;
    public List<bool> availableColors = new List<bool>();

    //PERKS
    public List<bool> perksState = new List<bool>();
    public int currentPerk;

    //LOG ENTRIES
    public List<bool> logItemsState = new List<bool>();
    public List<bool> logEnemiesState = new List<bool>();
    public List<bool> logBossesState = new List<bool>();

    //SETTINGS
    public bool masterActive;
    public float masterVolume;
    public bool musicActive;
    public float musicVolume;
    public bool sfxActive;
    public float sfxVolume;
    public bool postProcessActive;
    public bool rumbleActive;
}
