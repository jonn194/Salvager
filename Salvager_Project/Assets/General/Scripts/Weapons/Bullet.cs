using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float duration;
    public float durationVariation = 0;
    protected float _currentDuration;

    public int maxHits = 1;
    public int damage = 1;

    int _currentHits;
    void Update()
    {
        _currentDuration -= Time.deltaTime;

        Movement();
        CheckActive();
    }

    public void Setup()
    {
        float randomDur = Random.Range(0, durationVariation);

        _currentDuration = duration + randomDur;
        _currentHits = 0;
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void Movement()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    virtual public void CheckActive()
    {
        if(_currentDuration <= 0)
        {
            Deactivate();
        }
    }

    void AddHits()
    {
        _currentHits++;

        if(_currentHits >= maxHits)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.layer == K.LAYER_PLAYER_BULLET)
        {
            if (other.gameObject.layer == K.LAYER_ENEMY || other.gameObject.layer == K.LAYER_ENEMY_SERPENT || other.gameObject.layer == K.LAYER_BOSS || other.gameObject.layer == K.LAYER_BOSS_PROTECTOR)
            {
                if(other.TryGetComponent(out IDamageable target))
                {
                    target.GetDamage(damage);
                    AddHits();
                }
            }
            else if (other.gameObject.layer == K.LAYER_ENEMY_SHIELD)
            {
                Deactivate();
            }
        }
        else if(gameObject.layer == K.LAYER_ENEMY_BULLET)
        {
            if (other.gameObject.layer == K.LAYER_PLAYER)
            {
                other.GetComponent<PlayerStats>().GetDamage();
                AddHits();
            }
            else if (other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
            {
                other.GetComponent<PowerShield>().GetHit();
                AddHits();
            }
        }
    }
}
