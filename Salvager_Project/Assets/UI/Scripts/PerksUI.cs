using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PerksUI : MonoBehaviour
{
    public RectTransform buttonsContainer;
    public List<Button> _buttons;
    List<GlossaryItem> _items = new List<GlossaryItem>();

    public Image currentPerkImage;
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;

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
        int clickIndex = _buttons.IndexOf(btn);

        currentPerkImage.sprite = _items[clickIndex].itemIcon;
        titleTxt.text = _items[clickIndex].itemName;
        descriptionTxt.text = _items[clickIndex].description;
    }
}
