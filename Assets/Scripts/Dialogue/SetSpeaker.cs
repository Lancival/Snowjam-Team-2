using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

[RequireComponent(typeof(DialogueRunner))]

public class SetSpeaker : MonoBehaviour
{

	// DialogueRunner component that needs to handle SetSpeaker commands
	private DialogueRunner runner;

	// TextMeshPro component that contains the name of the speaker
	private TMP_Text nameText;

	// Mapping from SetSpeaker names in Yarn files to actual names in dialogue
	private Dictionary<string,string> names = new Dictionary<string,string>();

    void Awake() {
    	// Find components
    	runner = GetComponent<DialogueRunner>();
    	nameText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();

    	// Add new command handler for SetSpeaker in Yarn programs
        runner.AddCommandHandler("setSpeaker", HandleSpeaker);

        // Set up dictionary, probably a better way to this (having the same names in the Yarn files)
        names.Add("MC", "Ducky Claus");
        names.Add("d1", "Not Richard");
		names.Add("d2", "Austin");
		names.Add("d3", "Caroline");

    }

    void Update()
    {
    	// Maintain correct scale
    	if (transform.parent != null)
        {
            Vector3 parentScale = transform.parent.localScale;
            transform.localScale = Vector3.Scale(new Vector3(0.01f, 0.01f, 1), new Vector3(1.0f/parentScale.x, 1.0f/parentScale.y, 1));
        }
    }

    private void HandleSpeaker(string[] parameters) {

        // Take the first parameter, and use it to find the object
        string targetName = names[parameters[0]];
        GameObject target = GameObject.Find(targetName == "Ducky Claus" ? "Player" : targetName);

        // Log an error if we can't find it
        if (target == null) {
            Debug.LogError($"Unable to find {targetName} game object in scene.");
            return;
        }

        // Set the parent of the Dialogue Canvas (this game object) to the target
        transform.SetParent(target.transform, false);

        // Change the name in the dialogue box
        nameText.text = targetName;

    }
}
