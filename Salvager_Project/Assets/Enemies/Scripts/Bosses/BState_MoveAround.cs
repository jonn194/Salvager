using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_MoveAround : BossState
{
    [Header("Move Around State")]
    public BossProtectorManager protectors;
    public List<Vector3> targetLocations = new List<Vector3>();
    public float movementSpeed;
    public float rotationsSpeed = 5;

    Vector3 _originalRotation;
    int _currentIndex;

    public override void ExecuteState()
    {
        base.ExecuteState();
        _originalRotation = transform.parent.eulerAngles;
        _currentIndex = 0;
        targetLocations.Add(transform.parent.position);
    }

    public override void UpdateState()
    {
        if(Vector3.Distance(transform.parent.position, targetLocations[_currentIndex]) <= 0.5f)
        {
            if(_currentIndex < targetLocations.Count - 1)
            {
                _currentIndex++;
            }
            else
            {
                ResetRotation();
            }

        }
        else
        {
            Vector3 direction = targetLocations[_currentIndex] - transform.parent.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);

            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, toRotation, rotationsSpeed * Time.deltaTime);

            transform.parent.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

    void ResetRotation()
    {
        if (animationName != "None")
        {
            animator.SetBool(animationName, false);
        }

        if (Mathf.Abs(_originalRotation.y - transform.parent.eulerAngles.y) <= 2.5f)
        {
            transform.parent.eulerAngles = _originalRotation;
            FinishState();
        }
        else
        {
            //Vector3 targetRotation = Vector3.Lerp(transform.parent.eulerAngles, _originalRotation, 5 * Time.deltaTime);
            transform.parent.eulerAngles += transform.parent.up * rotationsSpeed * 100 * Time.deltaTime;
        }
    }

    public override void FinishState()
    {
        base.FinishState();
    }

    private void OnDrawGizmos()
    {
        if(targetLocations.Count > 0)
        {
            foreach(Vector3 target in targetLocations)
            {
                Gizmos.DrawWireSphere(target, 0.5f);
            }
        }
    }
}
