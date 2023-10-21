using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource, effectSource, tileSource, comboSource;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        musicSource.loop = true;
        musicSource.Play();
        effectSource.Stop();
        tileSource.Stop();
        comboSource.Stop();
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void ChangeSoundVolume(float volume)
    {
        effectSource.volume = volume;
        tileSource.volume = volume;
        comboSource.volume = volume;
    }

    public void playButtonClick()
    {
        tileSource.Play();
    }

    public void PlayComboSound()
    {
        comboSource.Play();
    }
}
