using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlossaryManager : MonoBehaviour
{
    [Header("Lists")]
    public List<GlossaryItem> enemies = new List<GlossaryItem>();
    public List<Sprite> enemySprites = new List<Sprite>();

    public List<GlossaryItem> items = new List<GlossaryItem>();
    public List<Sprite> itemSprites = new List<Sprite>();

    [Header("UI Elements")]
    public Image mainImage;
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
        ButtonSetup();
        CreateEnemies();
        CreateItems();
        ShowItems();
    }

    void Update()
    {
        if(_currentType == WindowType.Enemies)
        {
            mainImage.sprite = enemies[_currentIndex].image;
            titleTxt.text = enemies[_currentIndex].name;
            descriptionTxt.text = enemies[_currentIndex].description;
        }
        else if (_currentType == WindowType.Items)
        {
            mainImage.sprite = items[_currentIndex].image;
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
        for (int i = 0; i < items.Count; i++)
        {
            _buttons[i].gameObject.SetActive(true);
        }
    }

    void ShowEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
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

    void CreateEnemies()
    {
        enemies.Add(new GlossaryItem("Arrow Head", "This creature moves at a great speed in a straight line and won't stop", 2, enemySprites[0]));
        enemies.Add(new GlossaryItem("Rouge Head", "This creature moves from one side to the other while looking for scraps", 1, enemySprites[1]));
        enemies.Add(new GlossaryItem("Shield Head", "This creature is very slow, carring a huge shield which is hard to break", 6, enemySprites[2]));
        enemies.Add(new GlossaryItem("Stalker Head", "This creature will not stop until catching it's prey, and it will follow wherever it goes", 1, enemySprites[3]));
        enemies.Add(new GlossaryItem("Bomber Head", "This creature carries a pulse bomb which can cause great damage to it's surroundings", 2, enemySprites[4]));
        enemies.Add(new GlossaryItem("Sniper Head", "This creature will shoot any target from a great distance with it's destructive weapon", 4, enemySprites[5]));
        enemies.Add(new GlossaryItem("Double Head", "This creature will shoot in two directions at the same time with it's special weapon", 3, enemySprites[6]));
    }

    void CreateItems()
    {
        items.Add(new GlossaryItem("Scraps", "This item is used to rebuild broken ships and enable their use", 0, itemSprites[0]));
        items.Add(new GlossaryItem("Perk Fragment", "This item is used to unlock different perks for the ship", 0, itemSprites[1]));
        items.Add(new GlossaryItem("Energy Core", "This item is used to restore the ship's durability during the exploration", 1, itemSprites[2]));
        items.Add(new GlossaryItem("Shield", "This item creates a shield arround the ship to protect it from incoming attacks", 3, itemSprites[3]));
        items.Add(new GlossaryItem("Laser Module", "This item adds a temporal module that allows the ship to shoot a powerful laser", 5, itemSprites[4]));
        items.Add(new GlossaryItem("Tri-Shot Module", "This item adds a temporal module that allows the ship to shoot in three directions", 7, itemSprites[5]));
        items.Add(new GlossaryItem("Bomber Module", "This item adds a temporal module that allows the ship to shoot bombs far away", 4, itemSprites[6]));
    }

    public void ButtonClick(Button btn)
    {
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
