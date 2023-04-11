using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [Header("Dependencies")]
    public Collider damageCollider;
    public float warningDuration;
    public bool isBigLaser;
    public Animator anim;

    [Header("Visuals")]
    public LineRenderer lineRenderer;
    public ParticleSystem warningEffect;
    public ParticleSystem shootingEffect;

    public void StartLaser()
    {
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

        //shoot laser with animation
        if(isBigLaser)
        {
            anim.SetTrigger("ShootBig");
        }
        else
        {
            anim.SetTrigger("Shoot");
        }

        damageCollider.enabled = true;
    }

    public void StopLaser()
    {
        StopAllCoroutines();

        shootingEffect.Stop();

        //stop laser animation
        anim.SetTrigger("Stop");
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
