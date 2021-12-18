using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]

public class ShowLetter : MonoBehaviour
{

	// DialogueRunner component that needs to handle SetSpeaker commands
	private DialogueRunner runner;

	[SerializeField] private LetterManager manager;

    void Awake()
    {
        runner = GetComponent<DialogueRunner>();
        if (manager)
        {
        	runner.AddCommandHandler("showLetter", Show);
            runner.AddCommandHandler("endGame", EndGame);
        }
    }

    private void Show(string[] parameters)
    {
    	manager.ShowLetter();
    }

    private void EndGame(string[] parameters)
    {
        SceneLoader.instance.LoadNextScene();
    }
    
}
