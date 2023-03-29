using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_AlternatingLasers : BossState
{
    [Header("Alternating Laser State")]
    public List<EnemyLaser> set01 = new List<EnemyLaser>();
    public List<EnemyLaser> set02 = new List<EnemyLaser>();
}
