using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PerksUI : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform buttonsContainer;
    public List<Button> _buttons;
    List<GlossaryItem> _items = new List<GlossaryItem>();

    public Image currentPerkImage;
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;

    public Button buttonSelect;
    public Button buttonUnlock;
    public TMP_Text costTxt;

    int _clickIndex;

    public void Start()
    {
        ButtonSetup();
        ShowPerk(_buttons[0]);
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

            costTxt.text = "Unlock: " + GameManager.instance.perksPrices[_clickIndex];

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
    }

    public void SelectPerk()
    {
        GameManager.instance.currentPerk = _clickIndex;
    }

    public void UnlockPerk()
    {
        GameManager.instance.perksAmount -= GameManager.instance.perksPrices[_clickIndex];
        GameManager.instance.perksState[_clickIndex] = true;
    }
}
