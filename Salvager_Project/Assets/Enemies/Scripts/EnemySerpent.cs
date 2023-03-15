using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySerpent : Enemy
{
    [Header("Serpent")]
    public List<EnemySerpentPiece> pieces =  new List<EnemySerpentPiece>();

    public float horizontalSpeed;
    public float amplitude;
    public float frequency;
    public float followSpeed;

    public override void Start()
    {
        base.Start();

        foreach (EnemySerpentPiece piece in pieces)
        {
            piece.serpent = this;
        }
    }

    public override void Update()
    {
        base.Update();

        if(pieces.Count <= 0)
        {
            //score
            GameManager.instance.currentScore += score;
            DestroyEnemy(true);
        }

        BasicMovement();
        MovePieces();
    }

    void MovePieces()
    {
        float modifiedTime = Mathf.Sin(Time.time * frequency) * amplitude;

        pieces[0].transform.localPosition += transform.right * horizontalSpeed * modifiedTime;

        for (int i = 1; i < pieces.Count; i++)
        {
            //set the target position with the x value of the previous piece
            Vector3 targetPosition = new Vector3(pieces[i - 1].transform.localPosition.x, pieces[i].transform.localPosition.y, pieces[i].transform.localPosition.z);

            //lerp the piece position
            pieces[i].transform.localPosition = Vector3.Lerp(pieces[i].transform.localPosition, targetPosition, followSpeed * Time.deltaTime);
        }
    }


    public override void DestroyEnemy(bool byPlayer)
    {
        StopAllCoroutines();

        base.DestroyEnemy(byPlayer);
    }
}
