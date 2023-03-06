using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera topCamera;
    public Camera angleCamera;
    public float lerpSpeed;


    Vector3 mousePos;
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //topdownCamera
        mousePos = Input.mousePosition;
        mousePos.z = topCamera.transform.position.y;
        Vector3 topPos = topCamera.ScreenToWorldPoint(mousePos);

        Vector3 narrowPos = angleCamera.ScreenToWorldPoint(mousePos);

        Vector3 finalPos = new Vector3(narrowPos.x, topPos.y, topPos.z);

        transform.position = Vector3.Lerp(transform.position, finalPos, lerpSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
