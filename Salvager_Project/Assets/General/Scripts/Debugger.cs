using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Debugger : MonoBehaviour
{
    [Header("Frame Rate")]
    public TMP_Text text;
    float _deltaTime;

    [Header("Boundaries")]
    public bool showLeft;
    public bool showRight;
    public bool showTop;
    public bool showBottom;
    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1 / _deltaTime;
        text.text = "Fps: " + Mathf.Ceil(fps).ToString();
    }




    private void OnDrawGizmos()
    {
        if(showLeft || showRight || showTop || showBottom)
        {
            Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.y));

            Gizmos.color = Color.yellow;

            if (showLeft)
            {
                Gizmos.DrawWireSphere(new Vector3(screenBounds.x, 0, 0), 1);
            }
            if (showRight)
            {
                Gizmos.DrawWireSphere(new Vector3(-screenBounds.x, 0, 0), 1);
            }
            if (showTop)
            {
                Gizmos.DrawWireSphere(new Vector3(0, 0, screenBounds.z), 1);
            }
            if (showBottom)
            {
                Gizmos.DrawWireSphere(new Vector3(0, 0, -screenBounds.z), 1);
            }
        }

    }
}
