using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class OpenSettings : MonoBehaviour
{
	void OnSettings()
	{
		SettingsManager manager = SettingsManager.SettingsManagerInstance;
		if (manager != null)
		{
			if (manager.visible)
				manager.FadeOut();
			else
				manager.FadeIn();
		}
	}
}
