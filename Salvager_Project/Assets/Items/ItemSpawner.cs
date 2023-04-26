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

            Items newItem = Instantiate(items[randomItem], transform.parent);
            newItem.transform.position = itemPos.position;
            newItem.transform.rotation = Quaternion.identity;
        }
    }

    public void SpawnScraps(Transform itemPos)
    {
        int randomValue = Random.Range(1, 5);


        Items newItem = Instantiate(scraps, transform.parent);
        newItem.transform.position = itemPos.position;
        newItem.value = randomValue;
        newItem.transform.rotation = Quaternion.identity;
    }

    public void SpawnPerks(Transform itemPos)
    {
        int randomValue = Random.Range(2, 8);


        Items newItem = Instantiate(perks, transform.parent);
        newItem.transform.position = new Vector3(0,0,5);//itemPos.position;
        newItem.value = randomValue;
        newItem.transform.rotation = Quaternion.identity;
        
    }
}
