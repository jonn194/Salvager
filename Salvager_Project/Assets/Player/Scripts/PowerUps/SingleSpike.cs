using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpike : MonoBehaviour
{
    public int damage = 1;
    public PerkPowerSpikes perkSpikes;

    void Deactivate()
    {
        perkSpikes.TurnSpikeOff(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_ENEMY)
        {
            other.GetComponent<Enemy>().GetDamage(damage);
            Deactivate();
        }
        else if (other.gameObject.layer == K.LAYER_ENEMY_SERPENT)
        {
            other.GetComponent<EnemySerpentPiece>().DamagePiece(damage);
            Deactivate();
        }
    }
}
