using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_Death : BossState
{
    [Header("Death State")]
    public ParticleSystem deadParticle;

    public override void ExecuteState()
    {
        base.ExecuteState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FinishState()
    {
        EventHandler.instance.LevelUp();
        Destroy(transform.parent.gameObject);
    }
}
