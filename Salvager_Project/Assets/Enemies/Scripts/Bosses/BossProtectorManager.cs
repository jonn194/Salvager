using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProtectorManager : MonoBehaviour
{
    [Header("Protectors")]
    public BossProtector protectorPrefab;
    public List<BossProtector> spawnedProtectors = new List<BossProtector>();
    public BulletPool bulletPool;

    [Header("Positions")]
    public Transform circleParent;
    public Transform alignParent;
    public Transform expandParent;


    public List<Transform> circlePositions = new List<Transform>();
    public List<Transform> alignPositions = new List<Transform>();
    public List<Transform> expandPositions = new List<Transform>();

    [Header("Movement")]
    public float rotationSpeed = 5;
    public float speedModifier = 1;

    public void Start()
    {
        GetPositions();
        SpawnProtectors();
    }

    private void Update()
    {
        transform.eulerAngles += transform.up * rotationSpeed * speedModifier * Time.deltaTime;
    }

    void GetPositions()
    {
        //get circle
        for (int i = 0; i < circleParent.childCount; i++) 
        {
            circlePositions.Add(circleParent.GetChild(i).transform);
        }

        //get aligned
        for (int i = 0; i < alignParent.childCount; i++)
        {
            alignPositions.Add(alignParent.GetChild(i).transform);
        }

        //get expanded
        for (int i = 0; i < expandParent.childCount; i++)
        {
            expandPositions.Add(expandParent.GetChild(i).transform);
        }
    }

    void SpawnProtectors()
    {
        foreach(Transform t in circlePositions)
        {
            BossProtector bP = Instantiate(protectorPrefab, transform);
            bP.transform.position = t.position;
            bP.transform.rotation = t.rotation;
            bP.weapon.bulletPool = bulletPool;
            bP.Activate();
            spawnedProtectors.Add(bP);
        }
    }

    public void DestroyProtectors()
    {
        bulletPool.ClearAll();

        Destroy(gameObject);
    }

}
