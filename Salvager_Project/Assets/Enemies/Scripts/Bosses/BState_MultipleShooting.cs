using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BState_MultipleShooting : BossState
{
    public List<Shooting> weapons = new List<Shooting>();
    public float rotationSpeed;
    public float rotationDirection;
}
