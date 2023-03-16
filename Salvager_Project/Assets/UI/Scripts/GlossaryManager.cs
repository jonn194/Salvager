using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlossaryManager : MonoBehaviour
{
    public Transform GlossaryContainer;

    [Header("Lists")]
    public Transform enemiesParent;
    public GlossaryItem[] enemies;
    public Transform itemsParent;
    public GlossaryItem[] items;


    [Header("UI Elements")]
    public TMP_Text titleTxt;
    public TMP_Text valueTxt;
    public TMP_Text descriptionTxt;
    public VerticalLayoutGroup buttonGroup;
    public Button originalButton;
    public RectTransform scrollArea;
    List<Button> _buttons = new List<Button>();

    public enum WindowType { Items, Enemies, PrimeEnemies }

    WindowType _currentType = WindowType.Items;
    int _currentIndex;

    private void Start()
    {
        GlossaryContainer.gameObject.SetActive(true);

        enemies = enemiesParent.GetComponentsInChildren<GlossaryItem>();
        items = itemsParent.GetComponentsInChildren<GlossaryItem>();

        ButtonSetup();
        ShowItems();
        DeactivateAllItems();
        DeactivateAllItems();
    }

    void Update()
    {
        if (_currentType == WindowType.Enemies)
        {
            enemies[_currentIndex].gameObject.SetActive(true);
            titleTxt.text = enemies[_currentIndex].itemName;
            valueTxt.text = enemies[_currentIndex].value;
            descriptionTxt.text = enemies[_currentIndex].description;
        }
        else if (_currentType == WindowType.Items)
        {
            items[_currentIndex].gameObject.SetActive(true);
            titleTxt.text = items[_currentIndex].itemName;
            valueTxt.text = items[_currentIndex].value;
            descriptionTxt.text = items[_currentIndex].description;
        }
    }


    void ButtonSetup()
    {
        for (int i = 0; i < 10; i++)
        {
            Button btn = Instantiate(originalButton, buttonGroup.transform);
            btn.onClick.AddListener(delegate { ButtonClick(btn); });
            _buttons.Add(btn);
            btn.gameObject.SetActive(false);
        }
    }

    void ShowItems()
    {
        DeactivateAllEnemies();

        for (int i = 0; i < items.Length; i++)
        {
            _buttons[i].gameObject.SetActive(true);
        }
    }

    void ShowEnemies()
    {
        DeactivateAllItems();

        for (int i = 0; i < enemies.Length; i++)
        {
            _buttons[i].gameObject.SetActive(true);
        }
    }

    void SetButtonsOff()
    {
        foreach(Button b in _buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    /*void CreateEnemies()
    {
        enemies.Add(new GlossaryItem("Shield Head", "This creature is very slow, carring a huge shield which is hard to break", 6, enemySprites[2]));
        enemies.Add(new GlossaryItem("Stalker Head", "This creature will not stop until catching it's prey, and it will follow wherever it goes", 1, enemySprites[3]));
        enemies.Add(new GlossaryItem("Bomber Head", "This creature carries a pulse bomb which can cause great damage to it's surroundings", 2, enemySprites[4]));
        enemies.Add(new GlossaryItem("Sniper Head", "This creature will shoot any target from a great distance with it's destructive weapon", 4, enemySprites[5]));
        enemies.Add(new GlossaryItem("Double Head", "This creature will shoot in two directions at the same time with it's special weapon", 3, enemySprites[6]));
    }*/


    void DeactivateAllItems()
    {
        foreach (GlossaryItem i in items)
        {
            i.gameObject.SetActive(false);
        }
    }

    void DeactivateAllEnemies()
    {
        foreach (GlossaryItem i in enemies)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void DeactivateAll()
    {
        DeactivateAllEnemies();
        DeactivateAllItems();
        GlossaryContainer.gameObject.SetActive(false);
    }

    public void ButtonClick(Button btn)
    {
        if(_currentType == WindowType.Items)
        {
            items[_currentIndex].gameObject.SetActive(false);
        }
        else if(_currentType == WindowType.Enemies)
        {
            enemies[_currentIndex].gameObject.SetActive(false);
        }

        _currentIndex = _buttons.IndexOf(btn);
    }

    public void ChangeWindowType(int winType)
    {
        _currentIndex = 0;
        _currentType = (WindowType)winType;

        SetButtonsOff();

        if (_currentType == WindowType.Enemies)
        {
            scrollArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 500);
            ShowEnemies();
        }
        else if(_currentType == WindowType.Items)
        {
            scrollArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1500);
            ShowItems();
        }
    }
}
