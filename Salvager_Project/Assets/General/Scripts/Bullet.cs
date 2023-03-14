using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float duration;
    protected float _currentDuration;
    void Update()
    {
        _currentDuration -= Time.deltaTime;

        Movement();
        CheckActive();
    }

    public void Setup()
    {
        _currentDuration = duration;
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

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.layer == K.LAYER_PLAYER_BULLET)
        {
            if(other.gameObject.layer == K.LAYER_ENEMY)
            {
                other.GetComponent<Enemy>().GetDamage();
                Deactivate();
            }
        }
        else if(gameObject.layer == K.LAYER_ENEMY_BULLET)
        {
            if (other.gameObject.layer == K.LAYER_PLAYER)
            {
                other.GetComponent<PlayerStats>().GetDamage();
                Deactivate();
            }
            else if (other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
            {
                other.GetComponent<PowerShield>().GetHit();
                Deactivate();
            }
        }
    }
}
