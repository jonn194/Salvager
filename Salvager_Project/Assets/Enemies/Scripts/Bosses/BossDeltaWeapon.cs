using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeltaWeapon : MonoBehaviour
{
    public BossDelta mainBoss;
    public BossPiece[] pieces;
    public List<Vector3> localPositions = new List<Vector3>();

    private void Start()
    {
        pieces = transform.GetComponentsInChildren<BossPiece>();

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].mainBoss = mainBoss;
            localPositions.Add(pieces[i].transform.localPosition);

            if (i == 0)
            {
                Destroy(pieces[i].joint);
                pieces[i].rb.isKinematic = true;
            }
            else
            {
                pieces[i].joint.connectedBody = pieces[i - 1].rb;
            }
        }
    }
}
