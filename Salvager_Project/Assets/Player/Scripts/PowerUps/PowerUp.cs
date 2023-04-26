using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float maxLifetime;
    public float currentLifetime;

    public ParticleSystem effect;

    virtual public void Startup()
    {
        currentLifetime = maxLifetime;
    }

    virtual public void Refill()
    {
        currentLifetime = maxLifetime;
    }

    private void Update()
    {
        currentLifetime -= Time.deltaTime;

        if(currentLifetime <= 0)
        {
            Deactivate();
        }    
    }

    virtual public void Deactivate()
    {
        currentLifetime = 0;
        gameObject.SetActive(false);
    }
}
