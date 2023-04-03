using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEpsilon : Boss
{
    [Header("Boss Epsilon")]
    public BossState sSideMovement;
    public BossState sBombsAttack;
    public BossState sAlternatingLasers;
    public BossState sMoveAround;
    public BState_TailAttack sTailAttack;

    public BulletPool bombsPools;
    public BossDeltaWeapon originalWeapon;
    BossDeltaWeapon _spawnedWeapon;

    public override void Start()
    {
        _spawnedWeapon = Instantiate(originalWeapon, transform.parent);
        _spawnedWeapon.transform.position = transform.position;
        _spawnedWeapon.transform.rotation = transform.rotation;
        _spawnedWeapon.gameObject.SetActive(false);        
        sTailAttack.weapon = _spawnedWeapon;

        base.Start();
    }

    public override void SetStatesConnections()
    {
        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sBombsAttack, sAlternatingLasers, sMoveAround };

            sBombsAttack.possibleConnections = new List<BossState>() { sSideMovement, sAlternatingLasers, sMoveAround };

            sAlternatingLasers.possibleConnections = new List<BossState>() { sSideMovement, sBombsAttack, sMoveAround };

            sMoveAround.possibleConnections = new List<BossState>() { sSideMovement, sBombsAttack, sAlternatingLasers };

            sTailAttack.possibleConnections = new List<BossState>() { sSideMovement, sBombsAttack, sAlternatingLasers, sMoveAround };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sTailAttack);
            sBombsAttack.possibleConnections.Add(sTailAttack);
            sAlternatingLasers.possibleConnections.Add(sTailAttack);
            sTailAttack.possibleConnections.Add(sTailAttack);
        }
    }

    public override void DestroyBoss()
    {
        bombsPools.ClearAll();
        Destroy(_spawnedWeapon);

        base.DestroyBoss();
    }
}
