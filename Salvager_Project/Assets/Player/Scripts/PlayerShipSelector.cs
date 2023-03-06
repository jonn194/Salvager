using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipSelector : MonoBehaviour
{
    [Header("UI")]
    public Button buttonLeft;
    public Button buttonRight;

    [Header("Ships")]
    public Transform shipsContainer;
    MeshRenderer[] _ships;
    public Color lockedColor;

    [Header("Selector")]
    float _maxSelectorTime = 0.3f;
    float _currentSelectorTime;

    private void Start()
    {
        _ships = shipsContainer.GetComponentsInChildren<MeshRenderer>();
    }

    public void Update()
    {

        for (int i = 0; i < _ships.Length; i++)
        {
            if (GameManager.instance.shipsState[i])
            {
                _ships[i].material.SetColor("_Tint", Color.white);
            }
            else
            {
                _ships[i].material.SetColor("_Tint", lockedColor);
            }
        }
    }

    public void DeactivateShips()
    {
        for (int i = 0; i < _ships.Length; i++)
        {
            if(i != GameManager.instance.currentShip)
            {
                _ships[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateShips()
    {
        for (int i = 0; i < _ships.Length; i++)
        {
            _ships[i].gameObject.SetActive(true);
        }
    }

    public void MoveSelector(int direction)
    {
        if (direction < 0 && GameManager.instance.currentShip > 0)
        {
            _currentSelectorTime = 0;
            buttonLeft.interactable = false;
            buttonRight.interactable = false;
            GameManager.instance.currentShip--;
            StartCoroutine(SelectorCoroutine());
        }
        else if(direction > 0 && GameManager.instance.currentShip < _ships.Length - 1)
        {
            _currentSelectorTime = 0;
            buttonLeft.interactable = false;
            buttonRight.interactable = false;
            GameManager.instance.currentShip++;
            StartCoroutine(SelectorCoroutine());
        }

    }

    IEnumerator SelectorCoroutine()
    {
        while (true)
        {
            _currentSelectorTime += Time.deltaTime;
            
            shipsContainer.transform.localPosition = Vector3.Lerp(shipsContainer.transform.localPosition, new Vector3(GameManager.instance.currentShip * 5, 0, 0), _currentSelectorTime);
            
            if(_currentSelectorTime >= _maxSelectorTime)
            {
                buttonLeft.interactable = true;
                buttonRight.interactable = true;
                break;
            }


            yield return null;
        }
    }

}
