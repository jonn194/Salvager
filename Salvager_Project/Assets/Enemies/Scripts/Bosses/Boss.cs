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
    public Color damageTint;
    Color _originalTint;
    float _maxColorTime = 0.3f;
    float _currentColorTime;

    [Header("Dependencies")]
    public PlayerStats player;

    [Header("States")]
    public BossState sEnterLevel;
    public BossState sDeath;
    protected BossState _currentState;

    private void Start()
    {
        //set base values
        _originalTint = mesh.material.GetColor("_EmissionColor");
        currentLife = maxLife;
        
        //set event to change state
        EventHandler.instance.bossStateFinished += StateMachine;

        //set states connections
        SetStatesConnections();

        //set initial state
        _currentState = sEnterLevel;
        _currentState.ExecuteState();
    }

    public virtual void SetStatesConnections()
    {
        //set connections
    }

    private void Update()
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
            DestroyBoss();

            //return to avoid further checks
            return;
        }

        int randomState = Random.Range(0, _currentState.possibleConnections.Length);

        _currentState = _currentState.possibleConnections[randomState];
        _currentState.ExecuteState();
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

        if(currentLife <= 0)
        {
            StateMachine();
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

    void DestroyBoss()
    {
        GameManager.instance.currentScore += score;
        EventHandler.instance.LevelUp();
    }
}
