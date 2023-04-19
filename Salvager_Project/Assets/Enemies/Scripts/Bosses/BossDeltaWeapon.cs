using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeltaWeapon : MonoBehaviour, IDamageable
{
    public Boss mainBoss;

    public void GetDamage(int dmg)
    {
        mainBoss.GetDamage(dmg);
    }
}
