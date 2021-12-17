using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueUI))]

public class TextSpeedHandler : MonoBehaviour
{

	private DialogueUI ui;

    void Awake()
    {
		ui = GetComponent<DialogueUI>();

		Settings.onTextDelayChanged.AddListener(UpdateTextSpeed);        
    }

    void OnDestroy()
    {
    	Settings.onTextDelayChanged.RemoveListener(UpdateTextSpeed);
    }

    private void UpdateTextSpeed(float delay)
    {
    	ui.textSpeed = delay;
    }
}
