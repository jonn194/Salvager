using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeltaWeapon : MonoBehaviour, IDamageable
{
    public Boss mainBoss;
    public Renderer mesh;
    public Collider weaponCollider;
    public float fillSpeed = 1;
    public bool destroyed;
    float _currentFill = 1;

    public void GetDamage(int dmg)
    {
        mainBoss.GetDamage(dmg);
    }

    private void Update()
    {
        if(destroyed)
        {
            weaponCollider.enabled = false;
            _currentFill -= fillSpeed * Time.deltaTime;

            mesh.material.SetFloat("_Fill", _currentFill);

            if (_currentFill <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            other.GetComponent<PlayerStats>().GetDamage();
        }
        else if (other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
        {
            other.GetComponent<PowerShield>().GetHit();
        }
    }
}
