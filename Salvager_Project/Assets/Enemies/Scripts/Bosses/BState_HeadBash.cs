using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_HeadBash : BossState
{
    [Header("Head Bash State")]
    public float bashSpeed;

    public override void ExecuteState()
    {
        base.ExecuteState();
        FinishState();
    }
}
