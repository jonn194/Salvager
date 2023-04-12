using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleporter : Enemy
{
    [Header("Teleporter")]
    public float warningTime;
    public float teleportTime;

    public Transform targetPosition;

    public ParticleSystem warningParticle;
    public LineRenderer warningLine;
    public ParticleSystem teleportParticle;
    public ParticleSystem pointerParticle;

    [Header("Audio")]
    public AudioSource audioSource;

    public override void Start()
    {
        base.Start();

        StartCoroutine(WarningTimer());
    }

    IEnumerator WarningTimer()
    {
        yield return new WaitForSeconds(warningTime);
        warningLine.enabled = true;
        warningParticle.gameObject.SetActive(true);
        warningParticle.Play();
        StartCoroutine(TeleportTimer());
    }

    IEnumerator TeleportTimer()
    {
        yield return new WaitForSeconds(teleportTime);
        
        warningParticle.gameObject.SetActive(false);
        warningParticle.Stop();
        warningLine.enabled = false;
        
        teleportParticle.Play();

        if (audioSource)
        {
            audioSource.Play();
        }

        Teleport();
    }

    void Teleport()
    {
        transform.position = targetPosition.position;

        targetPosition.localPosition = new Vector3(-targetPosition.localPosition.x, targetPosition.localPosition.y, targetPosition.localPosition.z);
        warningLine.SetPosition(1, targetPosition.localPosition);
        pointerParticle.Play();

        StartCoroutine(WarningTimer());
    }

    public override void DestroyEnemy(bool byPlayer)
    {
        StopAllCoroutines();

        base.DestroyEnemy(byPlayer);
    }
}
