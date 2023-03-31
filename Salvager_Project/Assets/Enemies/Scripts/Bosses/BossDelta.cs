using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDelta : Boss
{
    [Header("Boss Delta")]
    public BossState sSideMovement;
    public BState_InOutLevel sInOutLevel;
    public BossState sStretchAttack;
    public BState_SwipeAttack sSwipeAttack;
    public BState_TailAttack sTailAttack;

    public Rigidbody headRB;
    public float piecesAmount = 5;
    public float pieceOffset = 3;
    public BossPiece originalPiece;
    List<BossPiece> spawnedPieces = new List<BossPiece>();
    public BossDeltaWeapon originalWeapon;
    List<BossDeltaWeapon> spawnedWeapons = new List<BossDeltaWeapon>();

    public override void Start()
    {
        SpawnPieces();
        SpawnWeapons();
        
        base.Start();
    }

    public override void SetStatesConnections()
    {
        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sInOutLevel };

            /*sSideMovement.possibleConnections = new List<BossState>() { sInOutLevel, sStretchAttack, sSwingAttack };

            sInOutLevel.possibleConnections = new List<BossState>() { sSideMovement, sStretchAttack, sSwingAttack };

            sStretchAttack.possibleConnections = new List<BossState>() { sSideMovement, sInOutLevel, sSwingAttack };

            sSwingAttack.possibleConnections = new List<BossState>() { sSideMovement, sInOutLevel, sStretchAttack };

            sTailAttack.possibleConnections = new List<BossState>() { sSideMovement, sInOutLevel, sStretchAttack, sSwingAttack };*/
        }
        else
        {
            sSideMovement.possibleConnections.Add(sTailAttack);
            sInOutLevel.possibleConnections.Add(sTailAttack);
            sStretchAttack.possibleConnections.Add(sTailAttack);
            sSwipeAttack.possibleConnections.Add(sTailAttack);
        }
    }

    void SpawnPieces()
    {
        for (int i = 0; i < piecesAmount; i++)
        {
            BossPiece newPiece = Instantiate(originalPiece, transform.parent);
            newPiece.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + pieceOffset * (i + 1));
            newPiece.transform.rotation = transform.rotation;
            newPiece.mainBoss = this;

            if (i == 0)
            {
                newPiece.joint.connectedBody = headRB;
            }
            else
            {
                newPiece.joint.connectedBody = spawnedPieces[i - 1].rb; 
            }

            spawnedPieces.Add(newPiece);
        }
    }

    void SpawnWeapons()
    {
        for (int i = 0; i < 3; i++)
        {
            BossDeltaWeapon newWeapon = Instantiate(originalWeapon, transform.parent);
            newWeapon.transform.position = transform.position;
            newWeapon.transform.rotation = transform.rotation;
            newWeapon.mainBoss = this;
            spawnedWeapons.Add(newWeapon);

            newWeapon.gameObject.SetActive(false);
        }

        sInOutLevel.weapons = spawnedWeapons;
        sSwipeAttack.weapon = spawnedWeapons[0];
        sTailAttack.weapon = spawnedWeapons[0];
    }
}
