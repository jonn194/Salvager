using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_TailAttack : BossState
{
    [Header("Tail Attack State")]
    public BossDeltaWeapon weapon;
    public float attackSpeed = 9;
    public float holdTime = 1;
    public List<Vector3> originPositions = new List<Vector3>();
    public List<Vector3> originRotations = new List<Vector3>();
    public List<Vector3> warningPositions = new List<Vector3>();
    public List<Vector3> attackPositions = new List<Vector3>();

    int _index;
    int _sequence;
    float _currentHoldTime;
    public override void ExecuteState()
    {
        base.ExecuteState();

        weapon.gameObject.SetActive(true);
        _index = Random.Range(0, originPositions.Count);
        _currentHoldTime = holdTime;

        weapon.transform.position = originPositions[_index];
        weapon.transform.eulerAngles = originRotations[_index];

    }

    public override void UpdateState()
    {
        if(_sequence == 0)
        {
            Move(warningPositions[_index], attackSpeed);
        }
        else if (_sequence == 1)
        {
            Hold();
        }
        else if (_sequence == 2)
        {
            Move(attackPositions[_index], attackSpeed);
        }
        else if (_sequence == 3)
        {
            Hold();
        }
        else if (_sequence == 4)
        {
            Move(originPositions[_index], -attackSpeed);
        }
        else if (_sequence == 5)
        {
            FinishState();
        }
    }

    void Move(Vector3 target, float speed)
    {
        weapon.transform.position -= weapon.transform.forward * speed * Time.deltaTime;

        if (CheckDistances(target, weapon.transform.position))
        {
            _sequence++;
            _currentHoldTime = holdTime;
        }
    }

    bool CheckDistances(Vector3 a, Vector3 b)
    {
        if (Vector3.Distance(a, b) <= 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Hold()
    {
        _currentHoldTime -= Time.deltaTime;

        if (_currentHoldTime <= 0)
        {
            _sequence++;
        }
    }

    public override void FinishState()
    {
        weapon.gameObject.SetActive(false);
        base.FinishState();
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 v in originPositions)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(v, 0.5f);
        }

        foreach (Vector3 v in warningPositions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(v, 0.5f);
        }

        foreach (Vector3 v in attackPositions)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(v, 0.5f);
        }
    }
}
