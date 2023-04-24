using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType { scraps, perk, energy, shield, trishot, laser, bomber}

    public ItemType type;

    public int value;
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

            switch(type)
            {
                case ItemType.scraps:
                    GameManager.instance.currentScraps += value;
                    itemsHandler.PlayEffects(false);
                    EventHandler.instance.CurencyPickup(true);
                    break;

                case ItemType.perk:
                    GameManager.instance.currentPerkCores += value;
                    itemsHandler.PlayEffects(false);
                    EventHandler.instance.CurencyPickup(false);
                    break;


                case ItemType.energy:
                    GameManager.instance.logItemsState[2] = true;
                    itemsHandler.PlayEffects(true);
                    itemsHandler.EnergyCore();
                    break;
                case ItemType.shield:
                    GameManager.instance.logItemsState[3] = true;
                    itemsHandler.PlayEffects(false);
                    itemsHandler.Shield();
                    break;
                case ItemType.trishot:
                    GameManager.instance.logItemsState[4] = true;
                    itemsHandler.PlayEffects(false);
                    itemsHandler.ModuleTrishot();
                    break;
                case ItemType.laser:
                    GameManager.instance.logItemsState[5] = true;
                    itemsHandler.PlayEffects(false);
                    itemsHandler.ModuleLaser();
                    break;
                case ItemType.bomber:
                    GameManager.instance.logItemsState[6] = true;
                    itemsHandler.PlayEffects(false);
                    itemsHandler.ModuleBomber();
                    break;
            }

            DestroyObject();
        }
    }
}
