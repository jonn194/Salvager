using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    public int maxLife;
    public int currentLife;
    public int score;

    [Header("Graphics")]
    public Renderer mesh;
    public Image lifeBar;
    public ParticleSystem hitParticle;
    public ParticleSystem deadParticle;
    public Color damageTint;
    Color _originalTint;
    float _maxColorTime = 0.3f;
    float _currentColorTime;

    [Header("Dependencies")]
    public PlayerStats player;
    public EnemySpawner spawner;

    private void Start()
    {
        _originalTint = mesh.material.GetColor("_EmissionColor");
        currentLife = maxLife;
    }

    void StateMachine()
    {

    }

    void UpdateLifebar()
    {
        lifeBar.fillAmount = (float)((currentLife * 100f) / maxLife) / 100f;
    }

    public void GetDamage(int damage)
    {
        //remove life
        currentLife -= damage;
        UpdateLifebar();

        //stop current particles
        hitParticle.Stop();

        //change tint
        mesh.material.SetColor("_EmissionColor", damageTint);

        StopCoroutine(ResetColor());
        _currentColorTime = 0;
        StartCoroutine(ResetColor());

        //check for destroy
        if (currentLife <= 0)
        {
            //score
            GameManager.instance.currentScore += score;

            //create particle
            DeadParticle();

            //destroy
            DestroyBoss();
        }

        //play new particles
        hitParticle.Play();
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

    void DeadParticle()
    {
        //create particle
        ParticleSystem particle = Instantiate(deadParticle, transform.parent);
        particle.transform.position = transform.position;
        particle.transform.rotation = transform.rotation;
    }

    void DestroyBoss()
    {

    }
}
