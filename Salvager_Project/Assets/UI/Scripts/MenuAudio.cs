using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public List<Button> mainButtons = new List<Button>();
    public List<Button> perkButtons = new List<Button>();
    public List<Toggle> toggles = new List<Toggle>();
    void Start()
    {
        foreach (Button btn in mainButtons)
        {
            btn.onClick.AddListener(delegate { PlaySound(1); });
        }

        foreach (Button btn in perkButtons)
        {
            btn.onClick.AddListener(delegate { PlaySound(0.5f); });
        }

        foreach(Toggle tgl in toggles)
        {
            tgl.onValueChanged.AddListener(delegate { PlaySound(1.5f); });
        }
    }

    void PlaySound(float pitch)
    {
        audioSource.pitch = pitch;
        audioSource.Play();
    }

}
