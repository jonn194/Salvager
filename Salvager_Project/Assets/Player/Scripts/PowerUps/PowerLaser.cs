using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLaser : PowerUp
{
    public float laserDuration;
    public float laserTimer;
    public float maxDamageRange = 18;
    public LineRenderer lineRenderer;
    public ParticleSystem particleEffect;
    public Collider damageCollider;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip laserClip;


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
        particleEffect.Play();

        if(audioSource)
        {
            audioSource.pitch = 1;
            audioSource.clip = laserClip;
            audioSource.Play();
        }

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
        particleEffect.Stop();
        StartCoroutine(LaserTimer());
    }

    public override void Deactivate()
    {
        lineRenderer.gameObject.SetActive(false);
        damageCollider.enabled = false;
        particleEffect.Stop();
        StopAllCoroutines();

        base.Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == K.LAYER_ENEMY)
        {
            if(other.transform.position.z < maxDamageRange)
            {
                other.gameObject.GetComponent<Enemy>().GetDamage(1);
            }
        }
    }
}
