using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public int maxLife;
    public int currentLife;
    public int score;
    bool _halfLifeChecked;

    [Header("Graphics")]
    public Renderer mesh;
    public Animator animator;
    public ParticleSystem hitParticle;
    public Color damageTint;
    Color _originalTint;
    float _maxColorTime = 0.3f;
    float _currentColorTime;

    [Header("Dependencies")]
    public PlayerStats player;
    public ItemSpawner itemSpawner;
    public BossCanvas bossCanvas;
    BossCanvas _spawnedCanvas;

    [Header("States")]
    public BossState sEnterLevel;
    public BState_Death sDeath;
    protected BossState _currentState;

    public virtual void Start()
    {
        //set base values
        _originalTint = mesh.material.GetColor("_EmissionColor");
        currentLife = maxLife;
        _spawnedCanvas = Instantiate(bossCanvas, transform.parent);
        _spawnedCanvas.transform.eulerAngles += transform.eulerAngles;
        _spawnedCanvas.targetBoss = this.transform;
        
        //set event to change state
        EventHandler.instance.bossStateFinished += StateMachine;

        //set states dependencies and connections
        SetStatesDependencies();
        SetStatesConnections();

        //set initial state
        _currentState = sEnterLevel;
        _currentState.ExecuteState();
    }

    public virtual void SetStatesConnections()
    {
        //set connections
    }

    public virtual void SetStatesDependencies()
    {
        //set dependencies
    }

    public virtual void Update()
    {
        _currentState.UpdateState();
    }

    public virtual void StateMachine()
    {
        if(currentLife <= 0)
        {          
            //change state to death
            _currentState = sDeath;
            _currentState.ExecuteState();

            //return to avoid further checks
            return;
        }

        int randomState = Random.Range(0, _currentState.possibleConnections.Count);

        _currentState = _currentState.possibleConnections[randomState];
        _currentState.ExecuteState();
        Debug.Log(_currentState);
    }


    public virtual void GetDamage(int damage)
    {
        //remove life
        currentLife -= damage;
        _spawnedCanvas.UpdateLifebar(currentLife, maxLife);

        //stop current particles
        hitParticle.Stop();

        //change tint
        mesh.material.SetColor("_EmissionColor", damageTint);

        //if life hits a specific value
        if(currentLife <= 0)
        {
            StateMachine();
        }
        else if(currentLife <= maxLife / 2 && !_halfLifeChecked)
        {
            //make sure it doesn't enter twice
            _halfLifeChecked = true;

            //add new states
            SetStatesConnections();
        }

        StopCoroutine(ResetColor());
        _currentColorTime = 0;
        StartCoroutine(ResetColor());

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

    public virtual void DestroyBoss()
    {
        itemSpawner.SpawnPerks(transform);

        GameManager.instance.currentScore += score;
        EventHandler.instance.LevelUp();

        Destroy(_spawnedCanvas.gameObject);
        Destroy(gameObject);
    }
}
