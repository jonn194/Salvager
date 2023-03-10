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

    [Header("Components")]
    public Shooting weapon;
    public PlayerMovement movement;

    [Header("Effects")]
    public ParticleSystem getHitVFX;
    public PostProcessHandler postprocess;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void GetDamage()
    {
        getHitVFX.Play();
        currentHP--;

        postprocess.SetMaxAberration();
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
