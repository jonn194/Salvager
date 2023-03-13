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
        transform.position += (transform.forward * verticalSpeed + transform.right * horizontalSpeed) * Time.deltaTime;

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Timeout();
        }
    }

    void PickedUp()
    {
        Destroy(gameObject);
    }

    void Timeout()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            PickedUp();
        }
    }
}
