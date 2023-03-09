using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyButton : MonoBehaviour
{
    public Button selectButton;
    public Button buyButton;
    public TMP_Text priceText;
    void Update()
    {
        if (!GameManager.instance.shipsState[GameManager.instance.currentShip])
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            priceText.text = "Buy: " + GameManager.instance.shipsPrices[GameManager.instance.currentShip].ToString();
        }
        else
        {
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
    }
}
