using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class OpenSettings : MonoBehaviour
{
	void OnSettings()
	{
		if (SettingsManager.SettingsManagerInstance != null)
			SettingsManager.SettingsManagerInstance.FadeIn();
	}
}
