using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossGamma : Boss
{
    [Header("Boss Gamma")]
    public BossState sSideMovement;
    public BState_MoveAround sMoveAround;
    public BState_AlignProtectors sAlignProtectors;
    public BState_ShootProtectors sShootProtectors;
    public BState_AlignProtectors sExpandProtectors;

    public BossProtectorManager protectorsPrefab;
    public float protectorFollowSpeed = 10;
    BossProtectorManager _currentProtector;

    public override void Start()
    {
        base.Start();

        _currentProtector = Instantiate(protectorsPrefab, transform.parent);
        _currentProtector.transform.position = transform.position;
        _currentProtector.transform.rotation = transform.rotation;
        sMoveAround.protectors = _currentProtector;
        sAlignProtectors.protectors = _currentProtector;
        sShootProtectors.protectors = _currentProtector;
        sExpandProtectors.protectors = _currentProtector;
    }

    public override void SetStatesConnections()
    {
        sShootProtectors.player = player;

        if (currentLife > maxLife / 2)
        {
            sEnterLevel.possibleConnections = new List<BossState>() { sMoveAround };

            sSideMovement.possibleConnections = new List<BossState>() { sMoveAround, sAlignProtectors, sShootProtectors };

            sMoveAround.possibleConnections = new List<BossState>() { sSideMovement, sAlignProtectors, sShootProtectors };

            sAlignProtectors.possibleConnections = new List<BossState>() { sSideMovement, sMoveAround, sShootProtectors };

            sShootProtectors.possibleConnections = new List<BossState>() { sSideMovement, sMoveAround, sAlignProtectors };

            sExpandProtectors.possibleConnections = new List<BossState>() { sSideMovement, sMoveAround, sAlignProtectors, sShootProtectors };
        }
        else
        {
            sSideMovement.possibleConnections.Add(sExpandProtectors);
            sMoveAround.possibleConnections.Add(sExpandProtectors);
            sAlignProtectors.possibleConnections.Add(sExpandProtectors);
            sShootProtectors.possibleConnections.Add(sExpandProtectors);
        }
    }

    public override void Update()
    {
        base.Update();

        if(_currentState == sEnterLevel || _currentState == sSideMovement || _currentState == sMoveAround || _currentState == sAlignProtectors)
        {
            Vector3 targetLocation = Vector3.Lerp(_currentProtector.transform.position, transform.position, protectorFollowSpeed * Time.deltaTime);
            _currentProtector.transform.position = targetLocation;
        }
    }

    public override void DestroyBoss()
    {
        _currentProtector.DestroyProtectors();

        base.DestroyBoss();
    }
}
