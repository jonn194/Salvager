using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProtectorManager : MonoBehaviour
{
    public List<Transform> ringLocations = new List<Transform>();
    public List<Transform> lineLocations = new List<Transform>();
    public List<Transform> expandLocations = new List<Transform>();

    public List<BossProtector> protectors = new List<BossProtector>();

    public float respawnTime;
    public float rotationSpeed;
}
