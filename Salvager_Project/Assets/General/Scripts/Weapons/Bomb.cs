using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    public float range;
    public ParticleSystem explosionEffect;
    public LayerMask detectionMask;

    public override void CheckActive()
    {
        if(_currentDuration <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] detection = Physics.OverlapSphere(transform.position, range, detectionMask);

        if(detection.Length > 0)
        {
            foreach(Collider c in detection)
            {
                c.gameObject.GetComponent<Enemy>().GetDamage(1);
            }
        }

        ParticleSystem particle = Instantiate(explosionEffect, transform.parent);
        particle.transform.position = transform.position;

        Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == K.LAYER_PLAYER_BULLET)
        {
            if (other.gameObject.layer == K.LAYER_ENEMY)
            {
                Explode();
            }
            else if (other.gameObject.layer == K.LAYER_ENEMY_SERPENT)
            {
                Explode();
            }
        }
        else if (gameObject.layer == K.LAYER_ENEMY_BULLET)
        {
            if (other.gameObject.layer == K.LAYER_PLAYER)
            {
            }
            else if (other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
            {
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
