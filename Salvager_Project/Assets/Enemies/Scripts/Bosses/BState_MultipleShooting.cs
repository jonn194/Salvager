using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_MultipleShooting : BossState
{
    [Header("Multiple Shooting State")]
    public List<Shooting> weapons = new List<Shooting>();
    public float rotationSpeed;
    public float rotationAngle;

    int _sequence;
    float _direction = 1;
    float _originalAngleY;
    float _currentTargetAngleY;
    float _currentInitialAngleY;

    public override void ExecuteState()
    {
        base.ExecuteState();

        //set sequence value
        _sequence = 0;

        //set initial rotation
        _originalAngleY = transform.parent.eulerAngles.y;

        //depending on the side, determine rotation direction
        if (transform.parent.position.x < 0)
        {
            _currentInitialAngleY = _originalAngleY + rotationAngle;
            _currentTargetAngleY = _originalAngleY - rotationAngle;
            _direction = -1;
        }
        else
        {
            _currentInitialAngleY = _originalAngleY - rotationAngle;
            _currentTargetAngleY = _originalAngleY + rotationAngle;
            _direction = 1;
        }
    }

    public override void UpdateState()
    {
        if (AnimationStartEnded())
        {
            if (_sequence == 0)
            {
                InitialRotation();
            }
            else if(_sequence == 1)
            {
                MainRotation();
            }
            else if (_sequence == 2)
            {
                ResetRotation();
            }
        }
    }

    void InitialRotation()
    {
        transform.parent.eulerAngles += transform.up * rotationSpeed * 3 * -_direction * Time.deltaTime;

        if (Mathf.Abs(_currentInitialAngleY - transform.parent.eulerAngles.y) <= 0.5f)
        {
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, _currentInitialAngleY, transform.parent.eulerAngles.z);

            //start the weapons
            foreach (Shooting s in weapons)
            {
                s.StartShooting();
            }
            
            _sequence++;
        }
    }

    void MainRotation()
    {
        transform.parent.eulerAngles += transform.up * rotationSpeed * _direction * Time.deltaTime;

        if (Mathf.Abs(_currentTargetAngleY - transform.parent.eulerAngles.y) <= 0.5f)
        {
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, _currentTargetAngleY, transform.parent.eulerAngles.z);

            foreach (Shooting s in weapons)
            {
                s.StopShooting();
            }

            _sequence++;
        }
    }

    void ResetRotation()
    {
        transform.parent.eulerAngles += transform.up * rotationSpeed * 3 * -_direction * Time.deltaTime;

        if (Mathf.Abs(_originalAngleY - transform.parent.eulerAngles.y) <= 0.5f)
        {
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, _originalAngleY, transform.parent.eulerAngles.z);

            FinishState();
        }
    }
}
