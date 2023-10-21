using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, soundslider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("soundVolume"))
        {
            LoadSoundVolume();
        }
        else
        {
            SetSoundVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        SoundManager.instance.ChangeMusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSoundVolume()
    {
        float volume = soundslider.value;
        SoundManager.instance.ChangeSoundVolume(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }

    private void LoadSoundVolume()
    {
        soundslider.value = PlayerPrefs.GetFloat("soundVolume");
        SetSoundVolume();
    }
}
