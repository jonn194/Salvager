using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Visuals")]
    public Renderer mesh;
    public Color damageTint = Color.white;
    public ParticleSystem hitEffect;
    public ParticleSystem deadEffect;
    
    Color _originalTint;
    float _maxColorTime = 0.5f;
    float _currentColorTime;
    float _hueReinforcement = 0.02f;
    AudioSource _damageASource;

    [Header("Stats")]
    public int maxLife;
    public int currentLife;
    public int score;
    public float moveSpeed;
    public float destroyPosition;
    public int reinforceLevel;

    int _lifeReinforcement = 1;
    bool _hasScraps;

    [Header("Dependencies")]
    public PlayerStats player;
    public ItemSpawner itemSpawner;
    public EnemySpawner spawner;
    public Vector3 screenBoundaries;


    public virtual void Start()
    {
        float randomScraps = Random.Range(0, 100);

        if(randomScraps <= 20)
        {
            _hasScraps = true;
            Material newMat = new Material(mesh.material);
            newMat.SetFloat("_Scraps", 1);
            mesh.material = newMat;
        }

        //set life adding the reinforcement level
        if(_lifeReinforcement * reinforceLevel <= maxLife * 2)
        {
            currentLife = maxLife + (_lifeReinforcement * reinforceLevel);
        }
        else
        {
            currentLife = maxLife * 2;
        }
        //set original emission
        _originalTint = mesh.material.GetColor("_EmissionColor");

        //set color based on reinforcement
        mesh.material.SetFloat("_HueShift", _hueReinforcement * reinforceLevel);

        //get damage audio
        _damageASource = hitEffect.gameObject.GetComponent<AudioSource>();
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
            if(_hasScraps)
            {
                itemSpawner.SpawnScraps(transform);
            }
            else
            {
                itemSpawner.SpawnItem(transform);
            }
        }
        spawner.RemoveEnemy(byPlayer);
        Destroy(gameObject);
    }

    void CheckDistance()
    {
        if (transform.position.z <= -destroyPosition || transform.position.z >= destroyPosition)
        {
            DestroyEnemy(false);
        }
    }

    public void GetDamage(int damage)
    {
        //remove life
        currentLife -= damage;

        //stop current particles
        hitEffect.Stop();

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
        hitEffect.Play();
        _damageASource.enabled = true;
        _damageASource.Play();
    }

    void DeadParticle()
    {
        //create particle
        ParticleSystem particle = Instantiate(deadEffect, transform.parent);
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
