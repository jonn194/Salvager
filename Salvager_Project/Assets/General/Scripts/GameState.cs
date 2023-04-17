using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameState : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;
    public float camAngleMenu;
    public float camAngleGameplay;
    public Transform camMenu;
    public Transform camGameplay;

    [Header("Player")]
    public PlayerStats playerStats;
    Vector3 playerOriginalPos;

    [Header("Enemies")]
    public EnemySpawner enemySpawner;
    EnemySpawner _currentSpawner;

    [Header("Temp")]
    public Transform tempParent;

    [Header("UI")]
    public TitleUI titleCanvas;
    public GameplayUI gameplayCanvas;
    public EndgameUI endgameCanvas;

    [Header("Perks")]
    public PerksHandler perksHandler;

    private void Start()
    {
        playerOriginalPos = playerStats.transform.position;
        EventHandler.instance.playerHPChanged += CheckPlayerHP;

        Setup();
    }

    private void CheckPlayerHP()
    {
        if(playerStats.playerDead)
        {
            playerStats.gameObject.SetActive(false);
            EndGame();
        }
    }

    public void PauseGame(int timeScale)
    {
        Time.timeScale = timeScale;

        if(timeScale <= 0)
        {
            GameManager.instance.gamePaused = true;
        }
        else
        {
            GameManager.instance.gamePaused = false;
        }
    }

    public void Setup()
    {
        //camera
        cam.transform.position = camMenu.position;
        cam.transform.rotation = camMenu.rotation;
        cam.fieldOfView = camAngleMenu;

        //player
        playerStats.StopPlayer(playerOriginalPos);

        //enemies
        _currentSpawner = Instantiate(enemySpawner, tempParent);
        CheckUnlocks();
    }

    public void StartGame()
    {
        GameManager.instance.gameStarted = true;

        //camera
        cam.transform.position = camGameplay.position;
        cam.transform.rotation = camGameplay.rotation;
        cam.fieldOfView = camAngleGameplay;

        //perks
        perksHandler.StartPerks();

        //player
        playerStats.StartPlayer(playerOriginalPos);


        //enemies
        _currentSpawner.transform.position = new Vector3(0, 0, 17);
        _currentSpawner.transform.eulerAngles = new Vector3(0, 180, 0);
        _currentSpawner.StartEnemies();
        _currentSpawner.player = playerStats;

        //score
        GameManager.instance.currentScore = 0;
        GameManager.instance.currentScraps = 0;
        GameManager.instance.currentPerkCores = 0;

        //UI
        GameplayStartUI();
    }

    public void EndGame()
    {
        GameManager.instance.gameStarted = true;

        //temp objects
        for (int i = 0; i < tempParent.childCount; i++)
        { 
            Transform t = tempParent.GetChild(i);
            Destroy(t.gameObject);
        }

        playerStats.playerDead = false;

        //UI
        CheckHighScore();
        CheckUnlocks();
        AddRewards();
        GameplayEndUI();
    }

    void CheckHighScore()
    {
        if(GameManager.instance.currentScore > GameManager.instance.highScore)
        {
            GameManager.instance.highScore = GameManager.instance.currentScore;
        }
    }

    void CheckUnlocks()
    {
        if(_currentSpawner.currentDificulty > GameManager.instance.maxDificulty)
        {
            GameManager.instance.maxDificulty = _currentSpawner.currentDificulty;
        }

        for (int i = 0; i <= _currentSpawner.maxEnemyIndex[_currentSpawner.currentDificulty]; i++)
        {
            GameManager.instance.logEnemiesState[i] = true;
        }
    }

    void AddRewards()
    {
        GameManager.instance.scrapAmount = GameManager.instance.currentScraps;
        GameManager.instance.perksAmount = GameManager.instance.currentPerkCores;
    }

    void GameplayStartUI()
    {
        titleCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
    }

    void GameplayEndUI()
    {
        gameplayCanvas.gameObject.SetActive(false);
        endgameCanvas.gameObject.SetActive(true);
        endgameCanvas.Setup();
    }
}
