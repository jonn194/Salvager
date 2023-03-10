using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraight : Enemy
{

    public override void Update()
    {
        base.Update();
        BasicMovement();
    }

}
