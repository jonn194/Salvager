using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_AlignProtectors : BossState
{
    [Header("Align State")]
    public BossProtectorManager protectors;

    public bool isExpansion;

    public float movementSpeed = 5;
    public float alignSpeed = 10;
    public float rotateSpeed = 10;


    List<Transform> targetTransforms = new List<Transform>();
    List<int> _returned = new List<int>();
    Vector3 _originalPosition;
    public override void ExecuteState()
    {
        animator.SetTrigger(animationName);
        _returned.Clear();
        protectors.speedModifier = 2;
        _originalPosition = transform.parent.position;

        if(isExpansion)
        {
            targetTransforms = protectors.expandPositions;
        }
        else
        {
            targetTransforms = protectors.alignPositions;
        }
    }

    public override void UpdateState()
    {
        stateDuration -= Time.deltaTime;

        if(stateDuration <= 0)
        {
            ReturnProtectors();
        }
        else
        {
            AlignProtectors();
        }    
    }

    void AlignProtectors()
    {
        for (int i = 0; i < protectors.alignPositions.Count; i++)
        {
            Vector3 targetPosition = Vector3.Lerp(protectors.spawnedProtectors[i].transform.position, targetTransforms[i].position, alignSpeed * Time.deltaTime);
            Vector3 targetRotation = Vector3.Lerp(protectors.spawnedProtectors[i].transform.eulerAngles, targetTransforms[i].eulerAngles, rotateSpeed * Time.deltaTime);

            protectors.spawnedProtectors[i].transform.position = targetPosition;
            protectors.spawnedProtectors[i].transform.eulerAngles = targetRotation;
        }

        if(!isExpansion)
        {
            Vector3 targetLocation = Vector3.Lerp(transform.parent.position, Vector3.zero, movementSpeed * Time.deltaTime);
            transform.parent.position = targetLocation;
        }
    }

    void ReturnProtectors()
    {
        for (int i = 0; i < protectors.circlePositions.Count; i++)
        {
            Vector3 targetPosition = Vector3.Lerp(protectors.spawnedProtectors[i].transform.position, protectors.circlePositions[i].position, alignSpeed * Time.deltaTime);
            Vector3 targetRotation = Vector3.Lerp(protectors.spawnedProtectors[i].transform.eulerAngles, protectors.circlePositions[i].eulerAngles, rotateSpeed * Time.deltaTime);

            protectors.spawnedProtectors[i].transform.position = targetPosition;
            protectors.spawnedProtectors[i].transform.eulerAngles = targetRotation;

            if (Vector3.Distance(protectors.spawnedProtectors[i].transform.position, protectors.circlePositions[i].position) <= 0.5f)
            {
                protectors.spawnedProtectors[i].transform.position = protectors.circlePositions[i].position;

                if(!_returned.Contains(i))
                {
                    _returned.Add(i);
                }
            }
        }

        Vector3 targetLocation = Vector3.Lerp(transform.parent.position, _originalPosition, movementSpeed * Time.deltaTime);
        transform.parent.position = targetLocation;

        if (_returned.Count == protectors.circlePositions.Count &&
            Vector3.Distance(transform.parent.position, _originalPosition) <= 0.5f)
        {
            FinishState();
        }
    }

    public override void FinishState()
    {
        transform.parent.position = _originalPosition;
        protectors.speedModifier = 1;
        EventHandler.instance.BossStateFinished();
    }
}
