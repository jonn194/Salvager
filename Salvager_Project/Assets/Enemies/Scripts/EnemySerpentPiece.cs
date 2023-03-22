using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySerpentPiece : MonoBehaviour
{
    [Header("Visuals")]
    public ParticleSystem hitParticle;
    public ParticleSystem deadParticle;
    public Renderer mesh;

    [Header("Stats")]
    public EnemySerpent serpent;
    public int maxLife;
    int _currentLife;


    private void Start()
    {
        _currentLife = maxLife;
    }

    public void DamagePiece(int damage)
    {
        hitParticle.Stop();

        _currentLife -= damage;

        if(_currentLife <= 0)
        {
            ParticleSystem particle = Instantiate(deadParticle, transform.parent);
            particle.transform.position = transform.position;
            particle.transform.rotation = transform.rotation;

            DestroyPiece();
        }
        
        hitParticle.Play();
    }

    void DestroyPiece()
    {
        serpent.pieces.Remove(this);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            other.GetComponent<PlayerStats>().GetDamage();
            DestroyPiece();
        }
        else if (other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
        {
            other.GetComponent<PowerShield>().GetHit();
            DestroyPiece();
        }
     }
}
