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
    public PlayerItemsHandler itemsHandler;
    public Collider damageCollider;

    [Header("Effects")]
    public ParticleSystem getHitVFX;
    public ParticleSystem deadVFX;
    public EffectsHandler postprocess;
    [ColorUsageAttribute(true, true)] public Color damageColor;
    [ColorUsageAttribute(true, true)] Color _originalColor = Color.white;

    Renderer _currentShip;
    float _invulnerableTime = 1;

    private void Start()
    {
        currentHP = maxHP;
        playerDead = false;
    }

    public void GetDamage()
    {
        //remove life
        currentHP--;

        //check death
        if(currentHP <= 0)
        {
            PlayerDeath();
        }

        //turn collider off
        damageCollider.enabled = false;
        _currentShip.material.SetColor("_Tint", damageColor);
        StartCoroutine(InvulnerableTimer());

        //play effect
        getHitVFX.Play();
        postprocess.PlayerHitEffects();

        EventHandler.instance.HPChanged();
    }

    IEnumerator InvulnerableTimer()
    {
        yield return new WaitForSeconds(_invulnerableTime);
        _currentShip.material.SetColor("_Tint", _originalColor);
        damageCollider.enabled = true;
    }

    public void PlayerDeath()
    {
        deadVFX.Play();

        movement.enabled = false;
        weapon.StopShooting();
        weapon.bulletPool.ClearAll();
        itemsHandler.DeactivateAll();
        StopAllCoroutines();

        StartCoroutine(SetDead());
    }

    IEnumerator SetDead()
    {
        yield return new WaitForSeconds(0.5f);
        playerDead = true;
        EventHandler.instance.HPChanged();
        _currentShip.material.SetColor("_Tint", _originalColor);
    }

    public void StartPlayer(Vector3 position)
    {
        movement.enabled = true;
        movement.Setup();
        _currentShip = movement.currentShipRenderer;

        _originalColor = _currentShip.material.GetColor("_Tint");

        currentHP = maxHP;
        EventHandler.instance.HPChanged();

        weapon.gameObject.SetActive(true);
        weapon.StartShooting();
        weapon.bulletPool.Generate();

        SetOriginalPosition(position);
    }

    public void StopPlayer(Vector3 position)
    {
        movement.ResetTilt();
        movement.enabled = false;
        
        currentHP = maxHP;
        EventHandler.instance.HPChanged();

        weapon.StopShooting();

        SetOriginalPosition(position);
    }

    public void SetOriginalPosition(Vector3 position)
    {
        transform.position = position; 
    }
}
