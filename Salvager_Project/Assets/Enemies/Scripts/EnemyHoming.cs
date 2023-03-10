using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoming : Enemy
{
    public override void Update()
    {
        base.Update();

        transform.LookAt(player.transform);
        BasicMovement();
    }
}
