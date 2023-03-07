using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP;
    public int currentHP;

    [Header("Components")]
    public Shooting weapon;
    public PlayerMovement movement;
    public PlayerShipSelector shipSelector;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void GetDamage()
    {
        currentHP--;
    }

    public void StartPlayer(Vector3 position)
    {
        shipSelector.DeactivateShips();
        movement.enabled = true;
        movement.Setup();
        currentHP = maxHP;
        weapon.gameObject.SetActive(true);
        weapon.StartShooting();
        SetOriginalPosition(position);
    }

    public void StopPlayer(Vector3 position)
    {
        shipSelector.ActivateShips();
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
