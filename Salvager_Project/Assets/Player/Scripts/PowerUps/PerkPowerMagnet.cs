using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPowerMagnet : MonoBehaviour
{
    public ParticleSystem effect;

    public float radius;
    public float strength;
    public LayerMask layerMask;

    Collider[] _currentDetection;

    private void Update()
    {
        _currentDetection = Physics.OverlapSphere(transform.position, radius, layerMask);

        if(_currentDetection.Length > 0)
        {
            MagnetizeObjects();
        }
    }

    void MagnetizeObjects()
    {
        foreach(Collider c in _currentDetection)
        {
            Vector3 direction = (c.transform.position - transform.position).normalized;

            c.transform.position -= direction * strength * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
