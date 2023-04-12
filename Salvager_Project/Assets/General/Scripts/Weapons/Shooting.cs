using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool playOnStart = true;

    public float shootTimer = 1;

    public BulletPool bulletPool;
    public ParticleSystem shootEffect;

    [Header("Audio")]
    public AudioSource shootAudio;
    public AudioClip shootClip;

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

        if(shootAudio)
        {
            shootAudio.clip = shootClip;
            shootAudio.Play();
            shootAudio.pitch = Random.Range(0.8f, 1.2f);
        }

        StartCoroutine(Shoot());
    }
}
