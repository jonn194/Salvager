using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySerpentPiece : MonoBehaviour, IDamageable
{
    [Header("Visuals")]
    public ParticleSystem hitParticle;
    public ParticleSystem deadParticle;
    public Renderer mesh;

    float _hueReinforcement = 0.02f;

    [Header("Stats")]
    public EnemySerpent serpent;
    public int maxLife;
    public int reinforceLevel;
    
    int _currentLife;
    int _lifeReinforcement = 1;

    [Header("Audio")]
    public AudioSource audioSource;

    private void Start()
    {
        //set life adding the reinforcement level
        _currentLife = maxLife + (_lifeReinforcement * reinforceLevel);
        //set color based on reinforcement
        mesh.material.SetFloat("_HueShift", _hueReinforcement * reinforceLevel);
    }

    public void GetDamage(int dmg)
    {
        hitParticle.Stop();

        _currentLife -= dmg;

        if(_currentLife <= 0)
        {
            ParticleSystem particle = Instantiate(deadParticle, transform.parent);
            particle.transform.position = transform.position;
            particle.transform.rotation = transform.rotation;

            DestroyPiece();
        }
        
        hitParticle.Play();

        if (audioSource)
        {
            audioSource.Play();
        }
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
