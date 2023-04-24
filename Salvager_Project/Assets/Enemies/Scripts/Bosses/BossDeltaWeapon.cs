using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeltaWeapon : MonoBehaviour, IDamageable
{
    public Boss mainBoss;
    public Renderer mesh;
    public Collider weaponCollider;
    public float fillSpeed = 1;
    public bool destroyed;
    float _currentFill = 1;

    public Color damageTint;
    Color _originalTint;
    float _maxColorTime = 0.5f;
    float _currentColorTime;

    public void GetDamage(int dmg)
    {
        mainBoss.GetDamage(dmg);

        //change tint
        mesh.material.SetColor("_EmissionColor", damageTint);

        StopCoroutine(ResetColor());
        _currentColorTime = 0;
        StartCoroutine(ResetColor());
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

    private void Update()
    {
        if(destroyed)
        {
            weaponCollider.enabled = false;
            _currentFill -= fillSpeed * Time.deltaTime;

            mesh.material.SetFloat("_Fill", _currentFill);

            if (_currentFill <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER)
        {
            other.GetComponent<PlayerStats>().GetDamage();
        }
        else if (other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
        {
            other.GetComponent<PowerShield>().GetHit();
        }
    }
}
