using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public bool generateOnAwake = true;
    public Bullet bulletPrefab;
    public int amount;

    public List<Bullet> spawnedBullets = new List<Bullet>();

    private void Awake()
    {
        if(generateOnAwake)
        {
            Generate();
        }
    }

    public void Generate()
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet b = Instantiate(bulletPrefab, transform.parent.parent);
            b.gameObject.SetActive(false);
            spawnedBullets.Add(b);
        }
    }

    public Bullet GetBullet(Transform requestTransform)
    {
        Bullet b = null;
        
        if (spawnedBullets.Count > 0)
        {
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
        }

        return b;
    }

    public void ClearAll()
    {
        if(spawnedBullets.Count > 0)
        {
            for (int i = amount - 1; i >= 0; i--)
            {
                Destroy(spawnedBullets[i].gameObject);
            }

            spawnedBullets.Clear();
        }
    }
}
