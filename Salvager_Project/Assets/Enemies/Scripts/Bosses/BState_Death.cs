using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_Death : BossState
{
    [Header("Death State")]
    public Boss bossRef;
    public Renderer mesh;
    public ParticleSystem deadParticle;
    public float fillSpeed;
    float _currentFill = 1;

    public override void ExecuteState()
    {
        base.ExecuteState();
    }

    public override void UpdateState()
    {
        _currentFill -= fillSpeed * Time.deltaTime;

        mesh.material.SetFloat("_Fill", _currentFill);

        if(_currentFill <= 0)
        {
            FinishState();
        }
    }

    public override void FinishState()
    {
        bossRef.DestroyBoss();
    }
}
