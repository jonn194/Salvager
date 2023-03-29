using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SpawnShield : BossState
{
    [Header("Spawn Shields State")]
    public List<Transform> shields = new List<Transform>();
    List<Vector3> _originalPositions = new List<Vector3>();
    public float movementSpeed;
    public float movementMaxX;
    public float shieldsSpeed;

    float _direction = 1;

    private void Start()
    {
        foreach(Transform t in shields)
        {
            _originalPositions.Add(t.position);
        }
    }

    public override void ExecuteState()
    {
        base.ExecuteState();
        foreach (Transform t in shields)
        {
            t.gameObject.SetActive(true);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        transform.parent.position += transform.parent.right * movementSpeed * _direction * Time.deltaTime;

        if (transform.parent.position.x <= -movementMaxX)
        {
            _direction = -1;
        }
        else if(transform.parent.position.x >= movementMaxX)
        {
            _direction = 1;
        }

        foreach (Transform t in shields) 
        {
            t.position += t.forward * shieldsSpeed * Time.deltaTime;
        }
    }

    public override void FinishState()
    {
        foreach(Transform t in shields)
        {
            t.gameObject.SetActive(false);
        }

        base.FinishState();
    }
}
