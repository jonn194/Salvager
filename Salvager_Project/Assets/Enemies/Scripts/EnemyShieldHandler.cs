using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldHandler : MonoBehaviour
{
    public List<EnemyShield> shields;
    public Vector3 originalPosition;

    public void StartShields()
    {
        foreach (EnemyShield shield in shields)
        {
            shield.gameObject.SetActive(true);
            shield.SpawnShield();
        }
    }

    public void EndShields()
    {
        foreach (EnemyShield shield in shields)
        {
            shield.DestroyShield();
        }

        StartCoroutine(DeactivateTimer());
    }

    IEnumerator DeactivateTimer()
    {
        yield return new WaitForSeconds(0.5f);

        transform.position = originalPosition;
        foreach (EnemyShield shield in shields)
        {
            shield.gameObject.SetActive(false);
        }
    }
}
