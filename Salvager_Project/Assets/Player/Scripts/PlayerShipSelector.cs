using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class PlayerShipSelector : MonoBehaviour
{
    [Header("UI")]
    public Button buttonLeft;
    public Button buttonRight;
    public TMP_Text valueTxt;

    [Header("Ships")]
    public Transform shipsContainer;
    public List<ParticleSystem> _thrusters = new List<ParticleSystem>();
    public Color lockedColor;
    List<MeshRenderer> _ships = new List<MeshRenderer>();

    public Button buttonSelect;
    public Button buttonUnlock;

    [Header("Colors")]
    public RectTransform colorButtonsContainer;
    public List<Button> colorButtons = new List<Button>();
    public Button colorButtonSelect;
    public Button colorButtonUnlock;
    public TMP_Text colorValueTxt;

    [Header("Selector")]
    float _maxSelectorTime = 0.3f;
    float _currentSelectorTime;


    int _currentShip;
    int _currentColor;

    private void Start()
    {
        for (int i = 0; i < shipsContainer.transform.childCount; i++)
        {
            _ships.Add(shipsContainer.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }

        for(int i = 0; i < _ships.Count; i++)
        {
            Transform t = _ships[i].transform.GetChild(0);
            _thrusters.Add(t.GetComponent<ParticleSystem>());
        }

        ColorButtonsSetup();
        DeactivateShips();
    }

    public void Update()
    {
        if(!GameManager.instance.gameStarted)
        {
            for (int i = 0; i < _ships.Count; i++)
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
    }

    public void DeactivateShips()
    {
        for (int i = 0; i < _ships.Count; i++)
        {
            if(i != GameManager.instance.currentShip)
            {
                _ships[i].gameObject.SetActive(false);
                _thrusters[i].Stop();
                _thrusters[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateShips()
    {
        for (int i = 0; i < _ships.Count; i++)
        {
            _ships[i].gameObject.SetActive(true);
            _thrusters[i].gameObject.SetActive(true);
            _thrusters[i].Play();
        }
    }

    public void MoveSelector(int direction)
    {
        if (direction < 0 && _currentShip > 0)
        {
            _currentSelectorTime = 0;
            buttonLeft.interactable = false;
            buttonRight.interactable = false;
            _currentShip--;
            SetButtons();
            StartCoroutine(SelectorCoroutine());
        }
        else if(direction > 0 && _currentShip < _ships.Count - 1)
        {
            _currentSelectorTime = 0;
            buttonLeft.interactable = false;
            buttonRight.interactable = false;
            _currentShip++;
            SetButtons();
            StartCoroutine(SelectorCoroutine());
        }
    }

    void SetButtons()
    {
        //SHIPS
        if (GameManager.instance.shipsState[_currentShip])
        {
            buttonSelect.gameObject.SetActive(true);
            buttonUnlock.gameObject.SetActive(false);

            //COLORS
            colorButtonsContainer.gameObject.SetActive(true);
            SetColorSelectButtons();
        }
        else
        {
            buttonSelect.gameObject.SetActive(false);
            buttonUnlock.gameObject.SetActive(true);
            valueTxt.text = "Unlock: " + GameManager.instance.shipsPrices[_currentShip];

            if(GameManager.instance.scrapAmount > GameManager.instance.shipsPrices[_currentShip])
            {
                buttonUnlock.interactable = true;
            }
            else
            {
                buttonUnlock.interactable = false;
            }

            //COLORS
            colorButtonsContainer.gameObject.SetActive(false);
            colorButtonSelect.gameObject.SetActive(false);
            colorButtonUnlock.gameObject.SetActive(false);
        }
    }

    IEnumerator SelectorCoroutine()
    {
        while (true)
        {
            _currentSelectorTime += Time.deltaTime;
            
            shipsContainer.transform.localPosition = Vector3.Lerp(shipsContainer.transform.localPosition, new Vector3(_currentShip * 5, 0, 0), _currentSelectorTime);
            
            if(_currentSelectorTime >= _maxSelectorTime)
            {
                buttonLeft.interactable = true;
                buttonRight.interactable = true;
                break;
            }

            yield return null;
        }
    }

    public void SetAtCurrent()
    {
        shipsContainer.transform.localPosition = new Vector3(GameManager.instance.currentShip * 5, 0, 0);
        _currentShip = GameManager.instance.currentShip;
        buttonUnlock.gameObject.SetActive(false);
    }

    public void SelectShip()
    {
        GameManager.instance.currentShip = _currentShip;
    }


    public void UnlockShip()
    {
        GameManager.instance.scrapAmount -= GameManager.instance.shipsPrices[_currentShip];
        GameManager.instance.shipsState[_currentShip] = true;
    }


    //COLORS
    void ColorButtonsSetup()
    {
        colorButtons = colorButtonsContainer.GetComponentsInChildren<Button>().ToList();

        colorButtons[0].onClick.AddListener(delegate { PreviewColor(0); });
        colorButtons[1].onClick.AddListener(delegate { PreviewColor(1); });
        colorButtons[2].onClick.AddListener(delegate { PreviewColor(2); });
        colorButtons[3].onClick.AddListener(delegate { PreviewColor(3); });
        colorButtons[4].onClick.AddListener(delegate { PreviewColor(4); });
        colorButtons[5].onClick.AddListener(delegate { PreviewColor(5); });
        colorButtons[6].onClick.AddListener(delegate { PreviewColor(6); });
        colorButtons[7].onClick.AddListener(delegate { PreviewColor(7); });

        SelectColor();
    }

    public void PreviewColor(int index)
    {
        int currentShip = GameManager.instance.currentShip;
        _currentColor = index;
        _ships[currentShip].material.SetFloat("_CurrentColor", index);
        GameManager.instance.shipsSelectedColor[currentShip] = index;

        SetColorSelectButtons();
    }

    void SetColorSelectButtons()
    {
        if (GameManager.instance.availableColors[_currentShip, _currentColor])
        {
            colorButtonSelect.gameObject.SetActive(true);
            colorButtonUnlock.gameObject.SetActive(false);
        }
        else
        {
            colorButtonSelect.gameObject.SetActive(false);
            colorButtonUnlock.gameObject.SetActive(true);

            if (_currentColor < 4)
            {
                colorValueTxt.text = "Color Unlock: " + GameManager.instance.basicColorPrice;

                if (GameManager.instance.scrapAmount > GameManager.instance.basicColorPrice)
                {
                    colorButtonUnlock.interactable = true;
                }
                else
                {
                    colorButtonUnlock.interactable = false;
                }
            }
            else
            {
                colorValueTxt.text = "Color Unlock: " + GameManager.instance.specialColorPrice;

                if (GameManager.instance.scrapAmount > GameManager.instance.specialColorPrice)
                {
                    colorButtonUnlock.interactable = true;
                }
                else
                {
                    colorButtonUnlock.interactable = false;
                }
            }
        }
    }

    public void SelectColor()
    {
        GameManager.instance.currentColor = _currentColor;
        
        //Turn check
        foreach(Button b in colorButtons)
        {
            b.transform.GetChild(0).gameObject.SetActive(false);
        }

        colorButtons[_currentColor].transform.GetChild(0).gameObject.SetActive(true);
    }


    public void UnlockColor()
    {
        if(_currentColor < 4)
        {
            GameManager.instance.scrapAmount -= GameManager.instance.basicColorPrice;
        }
        else
        {
            GameManager.instance.scrapAmount -= GameManager.instance.specialColorPrice;
        }

        GameManager.instance.availableColors[_currentShip, _currentColor] = true;
    }
}
