using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    [Header("Bomber")]
    public float distanceToExplode = 5;
    public ParticleSystem explosionParticle;
    public ParticleSystem focusParticle;

    bool _focusing = false;
    float _playerDistance;

    public override void Update()
    {
        base.Update();
        BasicMovement();

        _playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (_playerDistance < distanceToExplode && !_focusing)
        {
            _focusing = true;
            StartFocus();
        }
    }

    void StartFocus()
    {
        focusParticle.Play();

        StartCoroutine(FocusTimer());
    }

    IEnumerator FocusTimer()
    {
        yield return new WaitForSeconds(1f);
        Explode();
    }

    void Explode()
    {
        focusParticle.Stop();
        explosionParticle.Play();

        if (_playerDistance < distanceToExplode)
        {
            player.GetDamage();
        }

        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        _focusing = false;
    }

    public override void DestroyEnemy(bool byPlayer)
    {
        StopAllCoroutines();

        base.DestroyEnemy(byPlayer);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToExplode);
    }
}
