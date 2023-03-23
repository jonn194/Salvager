using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundWarnings : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        EventHandler.instance.playerPowerUp += OnPlayerPowerUp;
        EventHandler.instance.bossIncoming += OnDanger;
        EventHandler.instance.levelUp += OnLevelUp;
    }

    private void OnPlayerPowerUp()
    {
        anim.SetTrigger("PowerUp");
    }

    private void OnDanger()
    {
        anim.SetTrigger("Danger");
    }

    private void OnLevelUp()
    {
        anim.SetTrigger("LevelUp");
    }
}
