using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_MultipleShooting : BossState
{
    [Header("Multiple Shooting State")]
    public List<Shooting> weapons = new List<Shooting>();
    public float rotationSpeed;
    public float rotationAngle;

    float _sequence;
    float _currentTargetAngleY;
    float _currentInitialAngleY;
    float _currentAngleY;

    Vector3 _originalRotation;
    Vector3 _currentFullRotation;

    public override void ExecuteState()
    {
        base.ExecuteState();

        //set sequence value
        _sequence = 0;

        //set initial rotation
        _originalRotation = transform.parent.eulerAngles;
        _currentFullRotation = _originalRotation;
        _currentAngleY = _originalRotation.y;

        //depending on the side, determine rotation direction
        if (transform.parent.position.x < 0)
        {
            _currentInitialAngleY = _originalRotation.y + rotationAngle;
            _currentTargetAngleY = _originalRotation.y - rotationAngle;
        }
        else
        {
            _currentInitialAngleY = _originalRotation.y - rotationAngle;
            _currentTargetAngleY = _originalRotation.y + rotationAngle;
        }
    }

    public override void UpdateState()
    {
        if(_sequence == 0)
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

    void InitialRotation()
    {
        _currentAngleY = Mathf.LerpAngle(_currentAngleY, _currentInitialAngleY, rotationSpeed * 3 * Time.deltaTime);

        transform.parent.eulerAngles = new Vector3(_currentFullRotation.x, _currentAngleY, _currentFullRotation.z);

        if (Mathf.Round(_currentAngleY) == Mathf.Round(_currentInitialAngleY))
        {
            transform.parent.eulerAngles = new Vector3(_currentFullRotation.x, _currentInitialAngleY, _currentFullRotation.z);

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
        _currentAngleY = Mathf.LerpAngle(_currentAngleY, _currentTargetAngleY, rotationSpeed * Time.deltaTime);

        transform.parent.eulerAngles = new Vector3(_currentFullRotation.x, _currentAngleY, _currentFullRotation.z);

        if (Mathf.Round(_currentAngleY) == Mathf.Round(_currentTargetAngleY))
        {
            transform.parent.eulerAngles = new Vector3(_currentFullRotation.x, _currentTargetAngleY, _currentFullRotation.z);

            foreach (Shooting s in weapons)
            {
                s.StopShooting();
            }

            _sequence++;
        }
    }

    void ResetRotation()
    {
        _currentAngleY = Mathf.LerpAngle(_currentAngleY, _originalRotation.y, rotationSpeed * Time.deltaTime);

        transform.parent.eulerAngles = new Vector3(_currentFullRotation.x, _currentAngleY, _currentFullRotation.z);

        if (Mathf.Round(_currentAngleY) == Mathf.Round(_originalRotation.y))
        {
            transform.parent.eulerAngles = _originalRotation;

            FinishState();
        }
    }
}
