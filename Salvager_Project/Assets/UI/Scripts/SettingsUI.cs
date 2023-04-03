using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Audio Toggles")]
    public Toggle masterToggle;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    [Header("Audio Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Other Toggles")]
    public Toggle postProcessToggle;
    public Toggle rumbleToggle;

    [Header("Dependencies")]
    public AudioMixer mixer;
    public Volume volume;

    public void SettingsSetup()
    {
        masterToggle.isOn = GameManager.instance.masterActive;
        masterSlider.value = GameManager.instance.masterVolume;

        musicToggle.isOn = GameManager.instance.musicActive;
        musicSlider.value = GameManager.instance.musicVolume;

        sfxToggle.isOn = GameManager.instance.sfxActive;
        musicSlider.value = GameManager.instance.sfxVolume;

        postProcessToggle.isOn = GameManager.instance.postProcessActive;
        rumbleToggle.isOn = GameManager.instance.rumbleActive;
    }

    public void UpdateAudio()
    {
        if(masterToggle.isOn)
        {
            mixer.SetFloat("VolumeMaster", masterSlider.value);
        }
        else
        {
            mixer.SetFloat("VolumeMaster", -80);
        }

        if (musicToggle.isOn)
        {
            mixer.SetFloat("VolumeMusic", musicSlider.value);
        }
        else
        {
            mixer.SetFloat("VolumeMusic", -80);
        }

        if (sfxToggle.isOn)
        {
            mixer.SetFloat("VolumeSFX", sfxSlider.value);
        }
        else
        {
            mixer.SetFloat("VolumeSFX", -80);
        }

        GameManager.instance.masterActive = masterToggle.isOn;
        GameManager.instance.masterVolume = masterSlider.value;

        GameManager.instance.musicActive = musicToggle.isOn;
        GameManager.instance.musicVolume = musicSlider.value;

        GameManager.instance.sfxActive = sfxToggle.isOn;
        GameManager.instance.sfxVolume = musicSlider.value;
    }

    public void UpdatePostProcess()
    {
        volume.gameObject.SetActive(postProcessToggle.isOn);

        GameManager.instance.postProcessActive = postProcessToggle.isOn;
    }

    public void UpdateRumble()
    {

    }
}
