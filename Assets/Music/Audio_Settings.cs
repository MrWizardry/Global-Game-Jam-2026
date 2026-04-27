using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Carrega valores salvos (se houver)
        masterSlider.value = PlayerPrefs.GetFloat("Master", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("Effects", 0.75f);

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Master", value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Music", value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("Effects", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Effects", value);
    }
}
