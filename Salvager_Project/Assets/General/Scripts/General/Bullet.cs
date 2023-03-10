using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float duration;
    float _currentDuration;
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

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void Movement()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void CheckActive()
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
            if(other.gameObject.layer == K.LAYER_PLAYER_ENEMY)
            {
                other.GetComponent<Enemy>().GetDamage();
                Deactivate();
            }
        }
        else if(gameObject.layer == K.LAYER_PLAYER_ENEMY)
        {
            if (other.gameObject.layer == K.LAYER_PLAYER)
            {
                other.GetComponent<PlayerStats>().GetDamage();
                Deactivate();
            }
        }
    }
}
