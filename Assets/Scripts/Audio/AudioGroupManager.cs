using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioGroupManager : MonoBehaviour
{
	[SerializeField] private AudioMixer mixer;

    void Awake()
    {
        Settings.onMusicVolumeChanged.AddListener(UpdateMusicVolume);
        Settings.onSFXVolumeChanged.AddListener(UpdateSFXVolume);
    }

    void OnDestroy()
    {
    	Settings.onMusicVolumeChanged.RemoveListener(UpdateMusicVolume);
        Settings.onSFXVolumeChanged.RemoveListener(UpdateSFXVolume);
    }

    private void UpdateMusicVolume(float volume)
    {
    	mixer.SetFloat("Music Volume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20);
    }

    private void UpdateSFXVolume(float volume)
    {
    	mixer.SetFloat("SFX Volume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20);
    }
}
