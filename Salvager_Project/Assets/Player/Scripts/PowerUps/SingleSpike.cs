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
        if (other.gameObject.layer == K.LAYER_ENEMY || other.gameObject.layer == K.LAYER_ENEMY_SERPENT || other.gameObject.layer == K.LAYER_BOSS || other.gameObject.layer == K.LAYER_BOSS_PROTECTOR)
        {
            if (other.TryGetComponent(out IDamageable target))
            {
                target.GetDamage(damage);
                Deactivate();
            }
        }
        else if (other.gameObject.layer == K.LAYER_ENEMY_SHIELD)
        {
            Deactivate();
        }
    }
}
