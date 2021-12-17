using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]

public class SettingsManager : MonoBehaviour
{

	public static SettingsManager SettingsManagerInstance {get; private set;}

	[Header("Settings Sliders")]

		[Tooltip("Slider that controls the music volume.")]
		[SerializeField] private Slider musicVolumeSlider;

		[Tooltip("Slider that controls the music volume.")]
		[SerializeField] private Slider SFXVolumeSlider;

		[Tooltip("Slider that controls the music volume.")]
		[SerializeField] private Slider textDelaySlider;

	[Header("Settings Toggles")]

		[Tooltip("On button for timer.")]
		[SerializeField] private Button onButton;

		[Tooltip("Off button for timer.")]
		[SerializeField] private Button offButton;

	public bool visible {get; private set;}

	private Animator animator;

    void Awake()
    {
        SettingsManagerInstance = this;
        animator = GetComponent<Animator>();
        visible = false;

        Settings.onMusicVolumeChanged.AddListener(SetMusicVolumeSlider);
        Settings.onSFXVolumeChanged.AddListener(SetSFXVolumeSlider);
        Settings.onTextDelayChanged.AddListener(SetTextDelaySlider);
    }

    void OnDestroy()
    {
    	Settings.onMusicVolumeChanged.RemoveListener(SetMusicVolumeSlider);
        Settings.onSFXVolumeChanged.RemoveListener(SetSFXVolumeSlider);
        Settings.onTextDelayChanged.RemoveListener(SetTextDelaySlider);
    }

    public void UpdateMusicVolume(float volume)
    {
    	Settings.MUSIC_VOLUME = volume;
    }

    public void UpdateSFXVolume(float volume)
    {
    	Settings.SFX_VOLUME = volume;
    }

    public void UpdateTextDelay(float delay)
    {
    	Settings.TEXT_DELAY = delay;
    }

    public void ActivateTimer()
    {
    	Settings.TIMER_ACTIVE = true;
    }

    public void DeactivateTimer()
    {
    	Settings.TIMER_ACTIVE = false;
    }

    private void SetMusicVolumeSlider(float volume)
    {
    	musicVolumeSlider.value = volume;
    }

    private void SetSFXVolumeSlider(float volume)
    {
    	SFXVolumeSlider.value = volume;
    }

    private void SetTextDelaySlider(float volume)
    {
    	textDelaySlider.value = volume;
    }

    private void SetTimerButtons(bool active)
    {
    	onButton.transform.GetChild(0).gameObject.SetActive(active);
    	offButton.transform.GetChild(0).gameObject.SetActive(!active);
    }

    public void FadeIn()
    {

		SetMusicVolumeSlider(Settings.MUSIC_VOLUME);
        SetSFXVolumeSlider(Settings.SFX_VOLUME);
        SetTextDelaySlider(Settings.TEXT_DELAY);
        SetTimerButtons(Settings.TIMER_ACTIVE);

        visible = true;

    	animator.ResetTrigger("FadeOut");
    	animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {

    	visible = false;

    	animator.ResetTrigger("FadeIn");
    	animator.SetTrigger("FadeOut");
    }
    
}
