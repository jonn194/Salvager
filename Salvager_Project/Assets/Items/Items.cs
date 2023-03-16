using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType { scraps, perk, energy, shield, trishot, laser, bomber}

    public ItemType type;

    public float verticalSpeed;
    public float horizontalSpeed;
    public float lifetime = 2;

    private void Update()
    {
        transform.position += (-transform.forward * verticalSpeed + transform.right * horizontalSpeed) * Time.deltaTime;

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            PlayerItemsHandler itemsHandler = other.GetComponent<PlayerItemsHandler>();

            itemsHandler.PlayEffects();

            switch(type)
            {
                case ItemType.scraps:
                    GameManager.instance.scrapAmount++;
                    break;

                case ItemType.perk:
                    GameManager.instance.perksAmount++;
                    break;


                case ItemType.energy:
                    itemsHandler.EnergyCore();
                    break;
                case ItemType.shield:
                    itemsHandler.Shield();
                    break;
                case ItemType.trishot:
                    itemsHandler.ModuleTrishot();
                    break;
                case ItemType.laser:
                    itemsHandler.ModuleLaser();
                    break;
                case ItemType.bomber:
                    itemsHandler.ModuleBomber();
                    break;
            }

            DestroyObject();
        }
    }
}
