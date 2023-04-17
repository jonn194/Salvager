using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PerksUI : MonoBehaviour
{
    public TMP_Text perkCoresTxt;
    
    [Header("Perks Buttons")]
    public RectTransform buttonsContainer;
    public List<Button> _buttons;
    List<GlossaryItem> _items = new List<GlossaryItem>();

    [Header("Preview UI")]
    public Image currentPerkImage;
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public Image mainMenuPerkImage;

    [Header("Selection Buttons")]
    public Button buttonSelect;
    public Button buttonUnlock;
    public TMP_Text valueTxt;

    int _clickIndex;

    public void Start()
    {
        ButtonSetup();
        ShowPerk(_buttons[GameManager.instance.currentPerk]);
        UpdatePerksUI();
        SetMenuImage();
        PerksMarkers();
    }

    void ButtonSetup()
    {
        _buttons = buttonsContainer.GetComponentsInChildren<Button>().ToList();

        for (int i = 0; i < _buttons.Count; i++)
        {
            Button btn = _buttons[i];
            btn.onClick.AddListener(delegate { ShowPerk(btn); });
            _items.Add(_buttons[i].gameObject.GetComponent<GlossaryItem>());
            _buttons[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = _items[i].itemIcon;
        }

        UpdatePerksUI();
    }

    void ShowPerk(Button btn)
    {
        _clickIndex = _buttons.IndexOf(btn);

        if (GameManager.instance.perksState[_clickIndex])
        {
            buttonSelect.gameObject.SetActive(true);
            buttonUnlock.gameObject.SetActive(false);
        }
        else
        {
            buttonUnlock.gameObject.SetActive(true);
            buttonSelect.gameObject.SetActive(false);

            valueTxt.text = "Unlock: " + GameManager.instance.perksPrices[_clickIndex];

            if (GameManager.instance.perksAmount >= GameManager.instance.perksPrices[_clickIndex])
            {
                buttonUnlock.enabled = true;
            }
            else
            {
                buttonUnlock.enabled = false;
            }
        }

        currentPerkImage.sprite = _items[_clickIndex].itemIcon;
        titleTxt.text = _items[_clickIndex].itemName;
        descriptionTxt.text = _items[_clickIndex].description;
        UpdatePerksUI();
    }

    void UpdatePerksUI()
    {
        perkCoresTxt.text = "Cores: " + GameManager.instance.perksAmount.ToString();
    }

    public void SelectPerk()
    {
        GameManager.instance.currentPerk = _clickIndex;
        SetMenuImage();
        PerksMarkers();
    }

    void SetMenuImage()
    {
        mainMenuPerkImage.sprite = _items[GameManager.instance.currentPerk].itemIcon;
    }

    public void UnlockPerk()
    {
        GameManager.instance.perksAmount -= GameManager.instance.perksPrices[_clickIndex];
        GameManager.instance.perksState[_clickIndex] = true;
        UpdatePerksUI();
        ShowPerk(_buttons[_clickIndex]);

        PerksMarkers();
    }

    void PerksMarkers()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (i == GameManager.instance.currentPerk)
            {
                _buttons[i].transform.GetChild(1).gameObject.SetActive(true);
                _buttons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (GameManager.instance.perksState[i])
            {
                _buttons[i].transform.GetChild(1).gameObject.SetActive(false);
                _buttons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                _buttons[i].transform.GetChild(1).gameObject.SetActive(false);
                _buttons[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}
