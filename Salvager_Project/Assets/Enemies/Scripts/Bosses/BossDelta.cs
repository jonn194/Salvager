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

    public Collider mainCollider;
    public BossDeltaWeapon originalWeapon;
    List<BossDeltaWeapon> _spawnedWeapons = new List<BossDeltaWeapon>();
    public BossDeltaWeapon originalTailWeapon;
    BossDeltaWeapon _spawnedTailWeapon;

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

        sSideMovement.screenBoundaries = screenBoundaries;
        sInOutLevel.screenBoundaries = screenBoundaries;
        sSwipeAttack.screenBoundaries = screenBoundaries;
        sTailAttack.screenBoundaries = screenBoundaries;

        sInOutLevel.mainCollider = mainCollider;
        sInOutLevel.hitParticles = hitParticle;
        sDeath.bossRef = this;
    }

    void SpawnWeapons()
    {
        for (int i = 0; i < 3; i++)
        {
            BossDeltaWeapon newWeapon = Instantiate(originalWeapon, transform.parent);
            newWeapon.transform.position = transform.position;
            newWeapon.transform.rotation = transform.rotation;
            newWeapon.mainBoss = this;
            _spawnedWeapons.Add(newWeapon);

            newWeapon.gameObject.SetActive(false);
        }

        sInOutLevel.weapons = _spawnedWeapons;

        _spawnedTailWeapon = Instantiate(originalTailWeapon, transform.parent);
        _spawnedTailWeapon.transform.position = transform.position;
        _spawnedTailWeapon.transform.rotation = transform.rotation;
        _spawnedTailWeapon.mainBoss = this;
        _spawnedTailWeapon.gameObject.SetActive(false);

        sSwipeAttack.weapon = _spawnedTailWeapon;
        sTailAttack.weapon = _spawnedTailWeapon;
    }

    public override void CheckLife()
    {
        if(currentLife <= 0)
        {
            foreach (BossDeltaWeapon weapon in _spawnedWeapons)
            {
                weapon.destroyed = true;
            }

            _spawnedWeapons.Clear();

            _spawnedTailWeapon.destroyed = true;
        }


        base.CheckLife();
    }
    public override void DestroyBoss()
    {
        GameManager.instance.logBossesState[3] = true;

        base.DestroyBoss();
    }
}
