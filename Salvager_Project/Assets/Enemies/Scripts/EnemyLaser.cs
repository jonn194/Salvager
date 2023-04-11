using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [Header("Dependencies")]
    public LineRenderer lineRenderer;
    public Collider damageCollider;
    public float warningDuration;

    [Header("Visuals")]
    public ParticleSystem originEffect;
    public ParticleSystem warningEffect;
    public ParticleSystem shootingEffect;

    public void StartLaser()
    {
        originEffect.Play();
        warningEffect.gameObject.SetActive(true);
        warningEffect.Play();
        StartCoroutine(WarningTimer());
    }

    IEnumerator WarningTimer()
    {
        yield return new WaitForSeconds(warningDuration);
        ShootLaser();
    }

    void ShootLaser()
    {
        warningEffect.Stop();
        warningEffect.gameObject.SetActive(false);
        shootingEffect.Play();

        lineRenderer.gameObject.SetActive(true);
        damageCollider.enabled = true;
    }

    public void StopLaser()
    {
        StopAllCoroutines();

        originEffect.Stop();
        shootingEffect.Stop();
        lineRenderer.gameObject.SetActive(false);
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == K.LAYER_PLAYER)
        {
            other.GetComponent<PlayerStats>().GetDamage();
        }
    }
}
