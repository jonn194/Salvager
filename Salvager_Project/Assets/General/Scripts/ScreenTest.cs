using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour
{
    public bool showLeft;
    public bool showRight;
    public bool showTop;
    public bool showBottom;

    private void OnDrawGizmos()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.y));

        Gizmos.color = Color.yellow;

        if(showLeft)
        {
            Gizmos.DrawWireSphere(new Vector3(screenBounds.x, 0, 0), 1);
        }
        if(showRight)
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
