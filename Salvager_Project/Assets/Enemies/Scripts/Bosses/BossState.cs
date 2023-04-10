using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    public PlayerStats player;
    public float stateDuration;
    protected float _currentDuration;
    public Animator animator;
    public string animationName = "None";
    public float animationStartDuration = 0;
    [HideInInspector] public List<BossState> possibleConnections;

    protected float _currentAnimationStart;

    public virtual void ExecuteState()
    {
        //Debug.Log(this);
        _currentDuration = stateDuration;
        _currentAnimationStart = animationStartDuration;
        if (animationName != "None")
        {
            animator.SetBool(animationName, true);
        }
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

    public bool AnimationStartEnded()
    {
        if(_currentAnimationStart <= 0)
        {
            return true;
        }
        else
        {
            _currentAnimationStart -= Time.deltaTime;
            return false;
        }
    }

    public virtual void FinishState()
    {
        if(animationName != "None")
        {
            animator.SetBool(animationName, false);
        }
        EventHandler.instance.BossStateFinished();
    }
}
