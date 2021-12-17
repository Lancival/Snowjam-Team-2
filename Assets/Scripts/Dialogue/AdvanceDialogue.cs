using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
[DisallowMultipleComponent]

public class AdvanceDialogue : MonoBehaviour
{
    [SerializeField] private DialogueUI ui;

    void Awake()
    {
    	if (ui == null)
    	{
    		Debug.LogError("AdvanceDialogue script on Player is missing a reference to the DialogueUI component.");
    		Destroy(this);
    	}
    }

    void OnSpace()
    {
    	ui.MarkLineComplete();
    }
}
