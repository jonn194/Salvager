using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Visuals")]
    public Renderer mesh;
    public Color damageTint = Color.white;
    public ParticleSystem hitParticle;
    public ParticleSystem deadParticle;
    Color _originalTint;

    [Header("Stats")]
    public int maxLife;
    public int currentLife;
    public int score;
    public float moveSpeed;
    public float destroyPosition;

    [Header("Dependencies")]
    public PlayerStats player;
    public EnemySpawner spawner;

    float _maxColorTime = 0.5f;
    float _currentColorTime;

    public virtual void Start()
    {
        currentLife = maxLife;
        _originalTint = mesh.material.GetColor("_EmissionColor");
    }

    public virtual void Update()
    {
        CheckDistance();
    }

    protected void BasicMovement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public virtual void DestroyEnemy(bool byPlayer)
    {
        StopAllCoroutines();
        if(byPlayer)
        {
            spawner.itemSpawn.SpawnItem(transform);
        }
        spawner.RemoveEnemy(byPlayer);
        Destroy(gameObject);
    }

    void CheckDistance()
    {
        if (transform.position.z <= destroyPosition)
        {
            DestroyEnemy(false);
        }
    }

    public void GetDamage(int damage)
    {
        //remove life
        currentLife -= damage;

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
            DestroyEnemy(true);
        }

        //play new particles
        hitParticle.Play();
    }

    void DeadParticle()
    {
        //create particle
        ParticleSystem particle = Instantiate(deadParticle, transform.parent);
        particle.transform.position = transform.position;
        particle.transform.rotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == K.LAYER_PLAYER)
        {
            other.GetComponent<PlayerStats>().GetDamage();
            DeadParticle();
            DestroyEnemy(false);
        }
        else if(other.gameObject.layer == K.LAYER_PLAYER_SHIELD)
        {
            other.GetComponent<PowerShield>().GetHit();
            DeadParticle();
            DestroyEnemy(true);
        }
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
