using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifebar : MonoBehaviour
{
    public PlayerStats stats;
    public Image lifebar;

    private void Update()
    {    
        lifebar.fillAmount = (float)((stats.currentHP * 100f) / stats.maxHP) / 100f;
    }
}
