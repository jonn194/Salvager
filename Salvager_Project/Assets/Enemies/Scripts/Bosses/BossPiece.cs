using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPiece : MonoBehaviour
{
    public BossDelta mainBoss;
    public Joint joint;
    public Rigidbody rb;

    public void GetDamage(int dmg)
    {
        mainBoss.GetDamage(dmg);
    }
}
