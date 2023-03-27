using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance;

    public event Action playerHPChanged;
    public event Action playerPowerUp;
    public event Action bossIncoming;
    public event Action levelUp;

    public event Action bossStateFinished;

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

    public void HPChanged()
    {
        playerHPChanged?.Invoke();
    }

    public void PlayerPowerUp()
    {
        playerPowerUp?.Invoke();
    }

    public void BossIncoming()
    {
        bossIncoming?.Invoke();
    }

    public void LevelUp()
    {
        levelUp?.Invoke();
    }

    public void BossStateFinished()
    {
        bossStateFinished?.Invoke();
    }
}
