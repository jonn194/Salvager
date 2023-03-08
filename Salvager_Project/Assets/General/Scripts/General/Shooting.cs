using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool playOnStart = true;

    public float shootTimer = 1;

    public BulletPool bulletPool;

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
        StopCoroutine(Shoot());
        gameObject.SetActive(false);
    }


    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootTimer);
        bulletPool.GetBullet(transform);
        StartCoroutine(Shoot());
    }
}
