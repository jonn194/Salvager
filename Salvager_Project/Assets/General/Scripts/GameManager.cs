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
        availableColors[0, 0] = true;
        availableColors[2, 2] = true;
        availableColors[5, 0] = true;
        availableColors[5, 1] = true;
        availableColors[5, 2] = true;
    }

    void SaveGame()
    {

    }

    void LoadGame()
    {

    }


}
