using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPowerSpikes : MonoBehaviour
{
    public List<SingleSpike> spikes = new List<SingleSpike>();
    public List<ParticleSystem> spawnParticles = new List<ParticleSystem>();
    public int damage;
    public float timeInactive;

    int _deactivateCount;

    public void TurnSpikeOff(SingleSpike spike)
    {
        spike.gameObject.SetActive(false);
        _deactivateCount++;

        if(_deactivateCount >= spikes.Count)
        {
            StartCoroutine(ResetSpikes());
        }
    }

    void TurnSpikesOn()
    {
        foreach(SingleSpike s in spikes)
        {
            s.gameObject.SetActive(true);
        }

        foreach(ParticleSystem p in spawnParticles)
        {
            p.Play();
        }
    }

    IEnumerator ResetSpikes()
    {
        yield return new WaitForSeconds(timeInactive);
        TurnSpikesOn();
    }
}
