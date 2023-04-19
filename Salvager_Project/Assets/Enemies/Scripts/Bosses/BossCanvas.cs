using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvas : MonoBehaviour
{
    public Transform targetBoss;

    public Image lifeBar;
    void Update()
    {
        transform.position = targetBoss.position;
    }

    public void UpdateLifebar(int currentLife, int maxLife)
    {
        lifeBar.fillAmount = (float)((currentLife * 100f) / maxLife) / 100f;
    }
}
