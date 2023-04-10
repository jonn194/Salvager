using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeltaWeapon : MonoBehaviour, IDamageable
{
    public BossDelta mainBoss;

    public void GetDamage(int dmg)
    {
        mainBoss.GetDamage(dmg);
    }
}
