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

    public override void Start()
    {
        base.Start();

        StartCoroutine(WarningTimer());
    }

    IEnumerator WarningTimer()
    {
        yield return new WaitForSeconds(warningTime);
        warningLine.enabled = true;
        //warningParticle.Play();
        StartCoroutine(TeleportTimer());
    }

    IEnumerator TeleportTimer()
    {
        yield return new WaitForSeconds(teleportTime);
        //teleportParticle.Play();
        warningLine.enabled= false;
        Teleport();
    }

    void Teleport()
    {
        transform.position = targetPosition.position;

        targetPosition.localPosition = new Vector3(-targetPosition.localPosition.x, targetPosition.localPosition.y, targetPosition.localPosition.z);
        warningLine.SetPosition(1, targetPosition.localPosition);

        StartCoroutine(WarningTimer());
    }
}