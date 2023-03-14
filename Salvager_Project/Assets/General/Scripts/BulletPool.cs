using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int amount;

    public List<Bullet> spawnedBullets = new List<Bullet>();

    private void Awake()
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet b = Instantiate(bulletPrefab);
            b.gameObject.SetActive(false);
            spawnedBullets.Add(b);
        }
    }

    public Bullet GetBullet(Transform requestTransform)
    {
        Bullet b = null;

        for (int i = 0; i < amount; i++)
        {
            if (!spawnedBullets[i].gameObject.activeSelf)
            {
                b = spawnedBullets[i];
                b.transform.position = requestTransform.position;
                b.transform.rotation = requestTransform.rotation;
                b.gameObject.SetActive(true);
                b.Setup();
                break;
            }
        }

        return b;
    }
}
