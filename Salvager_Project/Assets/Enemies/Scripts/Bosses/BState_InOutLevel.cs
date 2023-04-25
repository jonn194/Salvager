using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_InOutLevel : BossState
{
    [Header("In Out State")]
    public float weaponMovementSpeed = 5;
    public float mainMovementSpeed = 3;
    public float retreivePosition = 20;

    public Collider mainCollider;
    public ParticleSystem hitParticles;
    public List<BossDeltaWeapon> weapons;
    public List<float> xPosition;
    public List<float> zPosition;
    public List<Vector3> rotations;

    int _sequence;
    Vector3 _originalPos;
    Vector3 _retreiveVector;
    List<int> _currentlyMoving = new List<int>();

    public override void ExecuteState()
    {
        base.ExecuteState();
        
        _sequence = 0;
        _originalPos = transform.parent.position;
        _retreiveVector = new Vector3(transform.parent.position.x, transform.parent.position.y, retreivePosition);
        _currentlyMoving.Add(0);

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].transform.position = new Vector3(xPosition[i], weapons[i].transform.position.y, zPosition[i]);
            weapons[i].transform.eulerAngles = rotations[i];
            weapons[i].gameObject.SetActive(true);
        }
    }

    public override void UpdateState()
    {
        if(_sequence == 0)
        {
            HideMainBoss();
        }
        else if (_sequence == 1)
        {
            MoveWeapons();
        }
        else if (_sequence == 2)
        {
            RestoreMainBoss();
        }
    }

    void HideMainBoss()
    {
        Vector3 temp = Vector3.Lerp(transform.parent.position, _retreiveVector, mainMovementSpeed * Time.deltaTime);
        transform.parent.position = temp;

        if (CheckDistance(transform.parent.position, _retreiveVector, 0.5f))
        {
            mainCollider.enabled = false;
            hitParticles.gameObject.SetActive(false);
            _sequence++;
        }
    }

    void MoveWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if(_currentlyMoving.Contains(i))
            {
                weapons[i].transform.position += weapons[i].transform.forward * weaponMovementSpeed * Time.deltaTime;
            }

            if(i != weapons.Count - 1)
            {
                if (Mathf.Abs((-xPosition[i] * 2) - weapons[i].transform.position.x) <= 0.5f)
                {
                    if (!_currentlyMoving.Contains(i + 1))
                    {
                        _currentlyMoving.Add(i + 1);
                    }
                }
            }
            else
            {
                if (Mathf.Abs(-xPosition[i] * 3 - weapons[i].transform.position.x) <= 0.5f)
                {
                    mainCollider.enabled = true;
                    hitParticles.gameObject.SetActive(true);
                    _sequence++;
                }
            }

        }
    }

    void RestoreMainBoss()
    {
        Vector3 temp = Vector3.Lerp(transform.parent.position, _originalPos, mainMovementSpeed * Time.deltaTime);
        transform.parent.position = temp;

        if (CheckDistance(transform.parent.position, _originalPos, 0.5f))
        {
            FinishState();
        }
    }

    public override void FinishState()
    {
        _currentlyMoving.Clear();
        foreach (BossDeltaWeapon w in weapons)
        {
            w.gameObject.SetActive(false);
        }

        base.FinishState();
    }
}
