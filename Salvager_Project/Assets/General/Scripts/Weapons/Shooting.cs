using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool playOnStart = true;

    public float shootTimer = 1;

    public BulletPool bulletPool;
    public ParticleSystem shootEffect;

    private void Start()
    {
        if(playOnStart)
        {
            StartCoroutine(Shoot());
        }
    }

    public void StartShooting()
    {
        StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }


    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootTimer);
        shootEffect.Stop();
        shootEffect.Play();
        bulletPool.GetBullet(transform);
        StartCoroutine(Shoot());
    }
}
