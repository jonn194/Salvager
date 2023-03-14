using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP;
    public int currentHP;
    public bool playerDead;

    [Header("Components")]
    public Shooting weapon;
    public PlayerMovement movement;

    [Header("Effects")]
    public ParticleSystem getHitVFX;
    public ParticleSystem deadVFX;
    public EffectsHandler postprocess;

    private void Start()
    {
        currentHP = maxHP;
        playerDead = false;
    }

    public void GetDamage()
    {
        getHitVFX.Play();
        currentHP--;

        if(currentHP <= 0)
        {
            PlayerDeath();
        }

        postprocess.PlayerHitEffects();
    }

    public void PlayerDeath()
    {
        deadVFX.Play();

        movement.enabled = false;
        weapon.StopShooting();

        StartCoroutine(SetDead());
    }

    IEnumerator SetDead()
    {
        yield return new WaitForSeconds(0.5f);
        playerDead = true;
    }

    public void StartPlayer(Vector3 position)
    {
        movement.enabled = true;
        movement.Setup();
        currentHP = maxHP;
        weapon.gameObject.SetActive(true);
        weapon.StartShooting();
        SetOriginalPosition(position);
    }

    public void StopPlayer(Vector3 position)
    {
        movement.ResetTilt();
        movement.enabled = false;
        currentHP = maxHP;
        weapon.StopShooting();
        SetOriginalPosition(position);
    }

    public void SetOriginalPosition(Vector3 position)
    {
        transform.position = position; 
    }
}
