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
    public List<MeshRenderer> ships = new List<MeshRenderer>();
    public Color lockedColor;

    [Header("Selector")]
    float _maxSelectorTime = 0.3f;
    float _currentSelectorTime;
    public void Update()
    {

        for (int i = 0; i < ships.Count; i++)
        {
            if (GameManager.instance.shipsState[i])
            {
                ships[i].material.SetColor("_Tint", Color.white);
            }
            else
            {
                ships[i].material.SetColor("_Tint", lockedColor);
            }
        }
    }

    public void DeactivateShips()
    {
        for (int i = 0; i < ships.Count; i++)
        {
            if(i != GameManager.instance.currentShip)
            {
                ships[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateShips()
    {
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].gameObject.SetActive(true);
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
        else if(direction > 0 && GameManager.instance.currentShip < ships.Count - 1)
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
