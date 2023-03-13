using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorSelector : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();

    private void Start()
    {
        buttons[0].onClick.AddListener(delegate { ButtonClick(0); });
        buttons[1].onClick.AddListener(delegate { ButtonClick(1); });
        buttons[2].onClick.AddListener(delegate { ButtonClick(2); });
        buttons[3].onClick.AddListener(delegate { ButtonClick(3); });

    }

    private void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (GameManager.instance.availableColors[GameManager.instance.currentShip, i])
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    void ButtonClick(float index)
    {
        Debug.Log(index);
    }
}
