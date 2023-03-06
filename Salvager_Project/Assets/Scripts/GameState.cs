using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Temporal")]
    public Transform tempParent;

    [Header("UI")]
    public GameObject UIMenu;
    public GameObject UIGameplay;


    private void Start()
    {
        playerOriginalPos = playerStats.transform.position;

        EndGame();
    }

    public void StartGame()
    {
        //camera
        cam.transform.position = camGameplay.position;
        cam.transform.rotation = camGameplay.rotation;
        cam.fieldOfView = camAngleGameplay;

        //player
        playerStats.StartPlayer(playerOriginalPos);

        //enemies
        EnemySpawner spawner = Instantiate(enemySpawner, tempParent);
        spawner.transform.position = new Vector3(0, 0, 17);
        spawner.transform.eulerAngles = new Vector3(0, 180, 0);
        spawner.StartEnemies();

        //UI
        UIMenu.SetActive(false);
        UIGameplay.SetActive(true);
    }

    public void EndGame()
    {
        //camera
        cam.transform.position = camMenu.position;
        cam.transform.rotation = camMenu.rotation;
        cam.fieldOfView = camAngleMenu;

        //player
        playerStats.StopPlayer(playerOriginalPos);

        //temp objects
        for(int i = 0; i < tempParent.childCount; i++)
        { 
            Transform t = tempParent.GetChild(i);
            Destroy(t);
        }

        //UI
        UIMenu.SetActive(true);
        UIGameplay.SetActive(false);
    }
}
