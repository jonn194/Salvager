using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLaser : PowerUp
{
    public float laserDuration;
    public float laserTimer;
    public LineRenderer lineRenderer;
    public Collider damageCollider;

    public override void Startup()
    {
        base.Startup();
        StartCoroutine(LaserTimer());
    }

    IEnumerator LaserTimer()
    {
        yield return new WaitForSeconds(laserTimer);
        StartLaser();
    }

    void StartLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        damageCollider.enabled = true;

        StartCoroutine(LaserDuration());
    }

    IEnumerator LaserDuration()
    {
        yield return new WaitForSeconds(laserDuration);
        EndLaser();
    }

    void EndLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        damageCollider.enabled = false;

        StartCoroutine(LaserTimer());
    }

    public override void Deactivate()
    {
        EndLaser();
        StopAllCoroutines();

        base.Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == K.LAYER_ENEMY)
        {
            other.gameObject.GetComponent<Enemy>().GetDamage(1);
        }
    }
}
