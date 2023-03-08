using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public Camera topCamera;
    public Camera angleCamera;
    public float lerpSpeed;

    [Header("Tilt")]
    public Transform shipsContainer;
    MeshRenderer[] _ships;
    public Transform _shipMesh;
    public float maxTilt;

    Vector3 _mousePos;
    Vector3 _defaultRotation = new Vector3(0, 180, 0);

    public void Setup()
    {
        _ships = shipsContainer.GetComponentsInChildren<MeshRenderer>();
        
        foreach(MeshRenderer m in _ships)
        {
            if(m.gameObject.activeSelf)
            {
                _shipMesh = m.transform;
            }
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //topdownCamera
        _mousePos = Input.mousePosition;
        _mousePos.z = topCamera.transform.position.y;
        Vector3 topPos = topCamera.ScreenToWorldPoint(_mousePos);

        Vector3 narrowPos = angleCamera.ScreenToWorldPoint(_mousePos);

        Vector3 finalPos = new Vector3(narrowPos.x, topPos.y, topPos.z);

        transform.position = Vector3.Lerp(transform.position, finalPos, lerpSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        TiltShip(finalPos);
    }

    void TiltShip(Vector3 mousePos)
    {
        float distX = mousePos.x - transform.position.x;
        float distZ = mousePos.z - transform.position.z;

        _shipMesh.transform.eulerAngles = new Vector3 (maxTilt * -distZ, _defaultRotation.y, maxTilt * distX);
    }

    public void ResetTilt()
    {
        if(_shipMesh)
        {
            _shipMesh.transform.eulerAngles = _defaultRotation;
        }
    }
}
