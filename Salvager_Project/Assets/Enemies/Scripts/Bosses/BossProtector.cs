using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProtector : MonoBehaviour
{
    public int maxLife;
    int _currentLife;

    public void Activate()
    {
        _currentLife = maxLife;
    }

    public void GetDamage(int dmg)
    {
        _currentLife -= dmg;
        if(_currentLife <= 0)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
