using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float maxLifetime;
    float _currentLifetime;

    public ParticleSystem effect;

    virtual public void Startup()
    {
        _currentLifetime = maxLifetime;
    }

    private void Update()
    {
        _currentLifetime -= Time.deltaTime;

        if(_currentLifetime <= 0)
        {
            Deactivate();
        }    
    }

    virtual public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
