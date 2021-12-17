using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Settings is a static class which stores global values needed in the game.
 * Some values are saved to PlayerPrefs so that they are preserved when the game is opened or closed.
 * These values are accessible as properties (ie you can access FONT_SCALE by using Settings.FONT_SCALE)
 * instead of through public functions.
 */

public static class Settings
{
	/* Gameplay State */
    public static bool PAUSED = false;

    /* Audio Settings */
	// Master Volume, affect all sounds and music
	public static UnityEvent<float> onMasterVolumeChanged = new UnityEvent<float>();
	public static float MASTER_VOLUME {
		get {return PlayerPrefs.GetFloat("Master", 1.0f);} // Default of full volume
		set {PlayerPrefs.SetFloat("Master", value);}
	}

	// Music Volume
	public static UnityEvent<float> onMusicVolumeChanged = new UnityEvent<float>();
	public static float MUSIC_VOLUME {
		get {return PlayerPrefs.GetFloat("Music", 1.0f);} // Default of full volume
		set {PlayerPrefs.SetFloat("Music", value);}
	}

	// SFX Volume
	public static UnityEvent<float> onSFXVolumeChanged = new UnityEvent<float>();
	public static float SFX_VOLUME {
		get {return PlayerPrefs.GetFloat("SFX", 1.0f);} // Default of full volume
		set {PlayerPrefs.SetFloat("SFX", value);}
	}

	/* Text Settings */
	// Text Speed
	public static UnityEvent<float> onTextDelayChanged = new UnityEvent<float>();
	public static float TEXT_DELAY {
		get {return PlayerPrefs.GetFloat("Delay", 0.025f);} // Default of full volume
		set {PlayerPrefs.SetFloat("Delay", value);}
	}

	/* Timer Settings */
	// Timer Active
	public static UnityEvent<bool> onTimerActiveChanged = new UnityEvent<bool>();
	public static bool TIMER_ACTIVE {
		get {return (PlayerPrefs.GetInt("Timer", 1) != 0 ? true : false);} // Timer active by default
		set {PlayerPrefs.SetInt("Timer", value ? 1 : 0);}
	}
}
