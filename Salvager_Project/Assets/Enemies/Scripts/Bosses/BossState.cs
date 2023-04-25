using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossState : MonoBehaviour
{
    public PlayerStats player;
    public float stateDuration;
    protected float _currentDuration;
    public Vector3 screenBoundaries;
    public Animator animator;
    public string animationName = "None";
    public float animationStartDuration = 0;
    public List<BossState> possibleConnections;

    protected float _currentAnimationStart;

    public virtual void ExecuteState()
    {
        //Debug.Log(this);
        //screenBoundaries = GameManager.instance.CalculateScreenBounds();

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

    public bool CheckRotation(Quaternion currentRotation, float targetAngle, float threshold)
    {
        if (Quaternion.Angle(Quaternion.Euler(0, targetAngle, 0), currentRotation) < threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckRotation(Quaternion currentRotation, Quaternion targetAngle, float threshold)
    {
        if (Quaternion.Angle(targetAngle, currentRotation) < threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckDistance(Vector3 a, Vector3 b, float threshold)
    {
        if (Vector3.Distance(a, b) <= threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
