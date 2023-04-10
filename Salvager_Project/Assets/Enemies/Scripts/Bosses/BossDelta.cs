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


    public BossDeltaWeapon originalWeapon;
    List<BossDeltaWeapon> spawnedWeapons = new List<BossDeltaWeapon>();

    public override void Start()
    {
        SpawnWeapons();
        
        base.Start();
    }

    public override void SetStatesConnections()
    {
        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sSideMovement };

            sSideMovement.possibleConnections = new List<BossState>() { sInOutLevel, sStretchAttack, sSwipeAttack };

            sInOutLevel.possibleConnections = new List<BossState>() { sSideMovement, sStretchAttack, sSwipeAttack };

            sStretchAttack.possibleConnections = new List<BossState>() { sSideMovement, sInOutLevel, sSwipeAttack };

            sSwipeAttack.possibleConnections = new List<BossState>() { sSideMovement, sInOutLevel, sStretchAttack };

            sTailAttack.possibleConnections = new List<BossState>() { sSideMovement, sInOutLevel, sStretchAttack, sSwipeAttack };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sTailAttack);
            sInOutLevel.possibleConnections.Add(sTailAttack);
            sStretchAttack.possibleConnections.Add(sTailAttack);
            sSwipeAttack.possibleConnections.Add(sTailAttack);
        }
    }

    public override void SetStatesDependencies()
    {
        sEnterLevel.animator = animator;
        sDeath.animator = animator;
        sSideMovement.animator = animator;

        sInOutLevel.animator = animator;
        sStretchAttack.animator = animator;
        sSwipeAttack.animator = animator;
        sTailAttack.animator = animator;
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
    public override void DestroyBoss()
    {
        foreach(BossDeltaWeapon weapon in spawnedWeapons)
        {
            Destroy(weapon);
        }

        spawnedWeapons.Clear();
        base.DestroyBoss();
    }
}
