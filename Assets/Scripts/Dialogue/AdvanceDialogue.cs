using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
[DisallowMultipleComponent]

public class AdvanceDialogue : MonoBehaviour
{
    [SerializeField] private DialogueUI ui;
    [SerializeField] private LetterManager manager;

    void Awake()
    {
    	if (ui == null)
    		Debug.LogWarning("AdvanceDialogue script on Player is missing a reference to the DialogueUI component.");
        if (manager == null)
            Debug.LogWarning("AdvanceDialogue script on Player is missing a reference to the LetterManager component.");
    }

    void OnSpace()
    {
        if (ui)
            ui.MarkLineComplete();
        if (manager)
            manager.HideLetter();
    }
}
