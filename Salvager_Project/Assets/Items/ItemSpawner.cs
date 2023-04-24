using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<Items> items = new List<Items>();
    public int itemChance = 30;

    public Items scraps;
    public Items perks;

    public void SpawnItem(Transform itemPos)
    {
        int randomChance = Random.Range(0, 100);

        if(randomChance < itemChance)
        {
            int randomItem = Random.Range(0, items.Count);
            float randomY = Random.Range(-10, 10);

            Items newItem = Instantiate(items[randomItem], transform.parent);
            newItem.transform.position = itemPos.position;

            Vector3 baseAngle = newItem.transform.eulerAngles;
            newItem.transform.eulerAngles = new Vector3(baseAngle.x, baseAngle.y + randomY, baseAngle.z);
        }
    }

    public void SpawnScraps(Transform itemPos)
    {
        int randomValue = Random.Range(1, 5);

        float randomY = Random.Range(-10, 10);
        Items newItem = Instantiate(scraps, transform.parent);
        newItem.transform.position = itemPos.position;
        newItem.value = randomValue;

        Vector3 baseAngle = newItem.transform.eulerAngles;
        newItem.transform.eulerAngles = new Vector3(baseAngle.x, baseAngle.y + randomY, baseAngle.z);
        
    }

    public void SpawnPerks(Transform itemPos)
    {
        int randomValue = Random.Range(2, 8);

        float randomY = Random.Range(-10, 10);
        Items newItem = Instantiate(perks, transform.parent);
        newItem.transform.position = itemPos.position;
        newItem.value = randomValue;

        Vector3 baseAngle = newItem.transform.eulerAngles;
        newItem.transform.eulerAngles = new Vector3(baseAngle.x, baseAngle.y + randomY, baseAngle.z);
        
    }
}
