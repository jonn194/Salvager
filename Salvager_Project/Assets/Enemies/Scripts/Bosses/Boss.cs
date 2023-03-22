using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    public int maxLife;
    public int currentLife;
    public int score;

    [Header("Graphics")]
    public Image lifeBar;

    [Header("Dependencies")]
    public PlayerStats player;
    public EnemySpawner spawner;

    void StateMachine()
    {

    }

    void UpdateLifebar()
    {
        lifeBar.fillAmount = (float)((currentLife * 100f) / maxLife) / 100f;
    }

    void DestroyBoss()
    {

    }
}
