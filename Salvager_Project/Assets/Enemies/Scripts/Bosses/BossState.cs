using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    public float stateDuration;
    float _currentDuration;
    [HideInInspector] public BossState[] possibleConnections;

    public virtual void ExecuteState()
    {
        //Debug.Log(this);
        _currentDuration = stateDuration;
    }

    public virtual void UpdateState()
    {
        if (DurationTimeout())
        {
            FinishState();
        }
    }

    public bool DurationTimeout()
    {
        _currentDuration -= Time.deltaTime;

        if (_currentDuration <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void FinishState()
    {
        EventHandler.instance.BossStateFinished();
    }
}
