using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProtector : MonoBehaviour
{
    [Header("Visuals")]
    public Renderer mesh;
    public Color damageTint = Color.white;
    public ParticleSystem hitParticle;

    Color _originalTint;
    float _maxColorTime = 0.5f;
    float _currentColorTime;

    [Header("Dependencies")]
    public Collider boxCollider;
    public Shooting weapon;

    [Header("Stats")]
    public int maxLife;
    public float respawnTime;
    
    int _currentLife;
    float _currentRespawnTime;
    bool _isActive = true;

    private void Update()
    {
        if(!_isActive)
        {
            _currentRespawnTime -= Time.deltaTime;

            if(_currentRespawnTime <= 0)
            {
                Reactivate();
            }
        }
    }

    public void Activate()
    {
        _currentLife = maxLife;
        _currentRespawnTime = respawnTime;
        weapon.StartShooting();
    }

    public void GetDamage(int dmg)
    {
        _currentLife -= dmg;

        //stop current particles
        hitParticle.Stop();
        hitParticle.Play();

        //change tint
        mesh.material.SetColor("_EmissionColor", damageTint);

        StopCoroutine(ResetColor());
        _currentColorTime = 0;
        StartCoroutine(ResetColor());

        if (_currentLife <= 0)
        {
            Deactivate();
        }
    }

    void Deactivate()
    {
        StopAllCoroutines();
        mesh.material.SetColor("_EmissionColor", _originalTint);
        mesh.gameObject.SetActive(false);
        boxCollider.enabled = false;
        _isActive = false;
        weapon.StopShooting();
    }

    void Reactivate()
    {
        Activate();
        mesh.gameObject.SetActive(true);
        boxCollider.enabled = true;
        _isActive = true;
    }

    IEnumerator ResetColor()
    {
        Color currentColor = mesh.material.GetColor("_EmissionColor");
        float progress = 0f;

        while (true)
        {
            _currentColorTime += Time.deltaTime;
            progress = ((_currentColorTime * 100) / _maxColorTime) / 100;

            currentColor = Color.Lerp(currentColor, _originalTint, progress);

            mesh.material.SetColor("_EmissionColor", currentColor);

            if (_currentColorTime >= _maxColorTime)
            {
                yield break;
            }

            yield return null;
        }
    }
}
