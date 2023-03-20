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

    [Header("Temp")]
    public Transform tempParent;

    [Header("UI")]
    public UIManager UIManager;

    [Header("Perks")]
    public PerksHandler perksHandler;

    private void Start()
    {
        playerOriginalPos = playerStats.transform.position;

        Setup();
    }

    private void Update()
    {
        if(playerStats.playerDead)
        {
            playerStats.gameObject.SetActive(false);
            EndGame();
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
    }

    public void StartGame()
    {
        //camera
        cam.transform.position = camGameplay.position;
        cam.transform.rotation = camGameplay.rotation;
        cam.fieldOfView = camAngleGameplay;

        //perks
        perksHandler.StartPerks();

        //player
        playerStats.StartPlayer(playerOriginalPos);


        //enemies
        EnemySpawner spawner = Instantiate(enemySpawner, tempParent);
        spawner.transform.position = new Vector3(0, 0, 17);
        spawner.transform.eulerAngles = new Vector3(0, 180, 0);
        spawner.StartEnemies();
        spawner.player = playerStats;

        //score
        GameManager.instance.currentScore = 0;

        //UI
        UIManager.GameplayStart();
    }

    public void EndGame()
    {
        //temp objects
        for (int i = 0; i < tempParent.childCount; i++)
        { 
            Transform t = tempParent.GetChild(i);
            Destroy(t.gameObject);
        }

        playerStats.playerDead = false;

        //UI
        CheckHighScore();
        UIManager.GameplayEnds();
    }

    void CheckHighScore()
    {
        if(GameManager.instance.currentScore > GameManager.instance.highScore)
        {
            GameManager.instance.highScore = GameManager.instance.currentScore;
        }
    }
}
