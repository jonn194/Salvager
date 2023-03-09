using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    public float distanceToExplote;

    public override void Update()
    {
        base.Update();

        if(Vector3.Distance(transform.position, player.transform.position) < distanceToExplote)
        {
            Explote();
        }
    }

    void Explote()
    {

    }
}
