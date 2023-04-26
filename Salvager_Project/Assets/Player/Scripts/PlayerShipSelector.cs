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
    public TMP_Text scrapsTxt;

    [Header("Ships")]
    public float offsetZ = 2;
    public Transform shipsContainer;
    public List<ParticleSystem> _thrusters = new List<ParticleSystem>();
    public Color lockedColor;
    List<MeshRenderer> _ships = new List<MeshRenderer>();
    public List<Color> shipsOriginalColors = new List<Color>();

    public Button buttonSelect;
    public Button buttonUnlock;

    [Header("Colors")]
    public RectTransform colorButtonsContainer;
    public List<Button> colorButtons = new List<Button>();
    public Button colorButtonSelect;
    public Button colorButtonUnlock;
    public TMP_Text colorValueTxt;

    [Header("Selector")]
    public ParticleSystem selectorEffect;
    float _maxSelectorTime = 0.3f;
    float _currentSelectorTime;


    int _currentShip;
    int _currentColor;

    private void Start()
    {
        //get the ships
        for (int i = 0; i < shipsContainer.transform.childCount; i++)
        {
            _ships.Add(shipsContainer.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }

        InitialSetup();

        UpdateScrapsUI();
        ColorButtonsSetup();
        DeactivateShips();
    }

    void InitialSetup()
    {
        //set the saved colors
        for (int i = 0; i < _ships.Count; i++)
        {
            Transform t = _ships[i].transform.GetChild(0);
            _thrusters.Add(t.GetComponent<ParticleSystem>());

            _ships[i].material.SetFloat("_CurrentColor", GameManager.instance.shipsSelectedColor[i]);
        }

        //relocate to align to selected ship
        _currentShip = GameManager.instance.currentShip;
        shipsContainer.transform.localPosition = new Vector3(_currentShip * 5, 0, offsetZ);

        //set color
        _currentColor = GameManager.instance.shipsSelectedColor[_currentShip];
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
            
            selectorEffect.transform.position =  new Vector3(_ships[GameManager.instance.currentShip].transform.position.x, selectorEffect.transform.position.y, selectorEffect.transform.position.z);
        }
    }

    void UpdateScrapsUI()
    {
        scrapsTxt.text = "Scraps: " + GameManager.instance.scrapAmount.ToString();
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

        //selector effect
        selectorEffect.Stop();
        selectorEffect.gameObject.SetActive(false);
    }

    public void ActivateShips()
    {
        for (int i = 0; i < _ships.Count; i++)
        {
            _ships[i].gameObject.SetActive(true);
            _thrusters[i].gameObject.SetActive(true);
            _thrusters[i].Play();
        }

        //selector effect
        selectorEffect.gameObject.SetActive(true);
        selectorEffect.Play();
        UpdateScrapsUI();
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
        }

        //COLORS
        EnableColorButtons();
        RecolorColorButtons();
    }

    IEnumerator SelectorCoroutine()
    {
        while (true)
        {
            _currentSelectorTime += Time.deltaTime;
            
            shipsContainer.transform.localPosition = Vector3.Lerp(shipsContainer.transform.localPosition, new Vector3(_currentShip * 5, 0, offsetZ), _currentSelectorTime);
            
            if(_currentSelectorTime >= _maxSelectorTime)
            {
                shipsContainer.transform.localPosition = new Vector3(_currentShip * 5, 0, offsetZ);
                buttonLeft.interactable = true;
                buttonRight.interactable = true;
                break;
            }

            yield return null;
        }
    }

    public void SetAtCurrent()
    {
        shipsContainer.transform.localPosition = new Vector3(GameManager.instance.currentShip * 5, 0, offsetZ);
        _currentShip = GameManager.instance.currentShip;
        buttonUnlock.gameObject.SetActive(false);
    }

    public void SelectShip()
    {
        //reset color from preview to selected before changing ship
        PreviewColor(GameManager.instance.shipsSelectedColor[_currentShip]);

        GameManager.instance.currentShip = _currentShip;
        //COLORS
        EnableColorButtons();
        ColorMarkers();
        PreviewColor(GameManager.instance.shipsSelectedColor[_currentShip]);
    }


    public void UnlockShip()
    {
        GameManager.instance.scrapAmount -= GameManager.instance.shipsPrices[_currentShip];
        GameManager.instance.shipsState[_currentShip] = true;

        UpdateScrapsUI();
        ColorMarkers();
    }


    //COLORS

    void EnableColorButtons()
    {
        if (GameManager.instance.currentShip == _currentShip)
        {
            //COLORS
            colorButtonsContainer.gameObject.SetActive(true);
            SetColorSelectButtons();
        }
        else
        {
            //COLORS
            colorButtonsContainer.gameObject.SetActive(false);
            colorButtonSelect.gameObject.SetActive(false);
            colorButtonUnlock.gameObject.SetActive(false);
        }

        RecolorColorButtons();
    }

    void RecolorColorButtons()
    {
        Vector4 currentHues = _ships[_currentShip].material.GetVector("_Hues");

        colorButtons[0].transform.GetChild(0).GetComponent<Image>().color = shipsOriginalColors[_currentShip];


        colorButtons[1].transform.GetChild(0).GetComponent<Image>().color = RecalculateHue(currentHues[1]);
        colorButtons[2].transform.GetChild(0).GetComponent<Image>().color = RecalculateHue(currentHues[2]);
        colorButtons[3].transform.GetChild(0).GetComponent<Image>().color = RecalculateHue(currentHues[3]);
    }

    Color RecalculateHue(float currentHue)
    {
        float h, s, v;
        Color.RGBToHSV(shipsOriginalColors[_currentShip], out h, out s, out v);
        float tempH;

        tempH = h + currentHue;


        if (tempH > 1)
        {
            tempH -= 1;
        }

        return Color.HSVToRGB(tempH, s, v);
    }

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

        colorButtons[_currentColor].GetComponent<Image>().color = Color.yellow;

        SelectColor();
        RecolorColorButtons();
    }

    public void PreviewColor(int index)
    {
        for (int i = 0; i < colorButtons.Count; i++)
        {
            if(i == index)
            {
                colorButtons[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                colorButtons[i].GetComponent<Image>().color = Color.white;
            }
        }

        int currentShip = GameManager.instance.currentShip;
        _currentColor = index;
        _ships[currentShip].material.SetFloat("_CurrentColor", index);

        SetColorSelectButtons();
    }

    void SetColorSelectButtons()
    {
        if (GameManager.instance.availableColors[(_currentShip * 8) + _currentColor])
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
        GameManager.instance.shipsSelectedColor[_currentShip] = _currentColor;
        ColorMarkers();
    }

    void ColorMarkers()
    {
        for(int i = 0; i < colorButtons.Count; i++)
        {
            if (i == GameManager.instance.shipsSelectedColor[_currentShip])
            {
                colorButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                colorButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (GameManager.instance.availableColors[(_currentShip * 8) + i])
            {
                colorButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                colorButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                colorButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                colorButtons[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }
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

        GameManager.instance.availableColors[(_currentShip * 8) + _currentColor] = true;
        UpdateScrapsUI();
        SetColorSelectButtons();
        ColorMarkers();
    }

    public void ResetAllColors()
    {
        for (int i = 0; i < _ships.Count; i++)
        {
            _ships[i].material.SetFloat("_CurrentColor", GameManager.instance.shipsSelectedColor[i]);
        }
    }
}
