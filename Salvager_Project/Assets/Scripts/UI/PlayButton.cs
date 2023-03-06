using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    public Button buyButton;
    public TMP_Text priceText;
    void Update()
    {
        if (!GameManager.instance.shipsState[GameManager.instance.currentShip])
        {
            playButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            priceText.text = "Buy: " + GameManager.instance.shipsPrices[GameManager.instance.currentShip].ToString();
        }
        else
        {
            playButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
    }
}
