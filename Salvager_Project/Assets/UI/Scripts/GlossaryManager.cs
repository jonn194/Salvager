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
    public TMP_Text descriptionTxt;
    public VerticalLayoutGroup buttonGroup;
    public Button originalButton;
    List<Button> _buttons = new List<Button>();

    public enum WindowType { Enemies, Items}

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
            titleTxt.text = enemies[_currentIndex].name;
            descriptionTxt.text = enemies[_currentIndex].description;
        }
        else if (_currentType == WindowType.Items)
        {
            items[_currentIndex].gameObject.SetActive(true);
            titleTxt.text = items[_currentIndex].name;
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
        enemies.Add(new GlossaryItem("Arrow Head", "This creature moves at a great speed in a straight line and won't stop", 2, enemySprites[0]));
        enemies.Add(new GlossaryItem("Rouge Head", "This creature moves from one side to the other while looking for scraps", 1, enemySprites[1]));
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
        items[_currentIndex].gameObject.SetActive(false);
        enemies[_currentIndex].gameObject.SetActive(false);

        _currentIndex = _buttons.IndexOf(btn);
    }

    public void ChangeWindowType(int winType)
    {
        _currentIndex = 0;
        _currentType = (WindowType)winType;

        SetButtonsOff();

        if (_currentType == WindowType.Enemies)
        {
            ShowEnemies();
        }
        else if(_currentType == WindowType.Items)
        {
            ShowItems();
        }
    }
}
