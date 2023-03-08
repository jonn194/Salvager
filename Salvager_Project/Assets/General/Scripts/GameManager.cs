using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;

    [Header("Stats")]
    public int scrapAmount;
    public int highScore;
    public int currentScore;

    [Header("Ships")]
    public List<bool> shipsState = new List<bool>();
    public List<int> shipsPrices = new List<int>();
    public int currentShip;



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
    }

    void SaveGame()
    {

    }

    void LoadGame()
    {

    }


}
