using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_SpawnShield : BossState
{
    [Header("Spawn Shields State")]
    public List<Transform> shields = new List<Transform>();
    List<Vector3> _originalPositions = new List<Vector3>();
    public float shieldsSpeed;


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
            t.gameObject.SetActive(false);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

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
