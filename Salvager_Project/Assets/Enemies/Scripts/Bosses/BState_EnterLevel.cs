using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_EnterLevel : BossState
{
    [Header("Enter Level State")]
    public float speed;
    public float targetZ;


    public override void UpdateState()
    {
        if(transform.parent.position.z > targetZ)
        {
            transform.parent.position += transform.parent.forward * speed * Time.deltaTime;
        }
        else
        {
            FinishState();
        }
    }
}
