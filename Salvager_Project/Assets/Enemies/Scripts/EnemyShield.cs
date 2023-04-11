using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    [Header("Visuals")]
    public Animator anim;
    public Renderer mesh;
    public ParticleSystem effect;
    public float shieldFill = 0;
    public bool playParticles;

    [Header("Components")]
    public Collider mainCollider;

    public void SpawnShield()
    {
        anim.SetTrigger("Spawn");
        mainCollider.enabled = true;
    }

    public void DestroyShield()
    {
        anim.SetTrigger("Destroy");
        mainCollider.enabled = false;
    }


    private void Update()
    {
        mesh.material.SetFloat("_Fill", shieldFill);

        if (playParticles && !effect.isPlaying)
        {
            effect.Play();
        }
        else if(!playParticles && effect.isPlaying)
        {
            effect.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            other.gameObject.GetComponent<PlayerStats>().GetDamage();
        }
    }
}
