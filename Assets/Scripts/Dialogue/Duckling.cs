using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Duckling : MonoBehaviour
{
    [Header("Ingredients Wanted")]

        [Tooltip("Number of slices of toast this duckling wants.")]
        [SerializeField] private int breadWanted = 2;

        [Tooltip("Number of jars of jam this duckling wants.")]
        [SerializeField] private int jamWanted = 1;

        [Tooltip("Which kind of fruit this duckling wants for jam.")]
        [SerializeField] private JamTypes jamType = JamTypes.None;

    [Header("YarnSpinner")]

        [Tooltip("Dialogue to display if you deliver the toast in time.")]
        [SerializeField] private YarnProgram completed;

        [Tooltip("Name of the starting node for the completed YarnProgram.")]
        [SerializeField] private string startNodeCompleted;

        [Tooltip("Dialogue to display if you run out of time.")]
        [SerializeField] private YarnProgram outOfTime;

        [Tooltip("Name of the starting node for the outOfTime YarnProgram.")]
        [SerializeField] private string startNodeOutOfTime;

        [Tooltip("DialogueRunner component for this scene. Should be found on the Dialogue Canvas game object.")]
        [SerializeField] private DialogueRunner runner;

    private bool finished = false;

    void Awake()
    {
        // Check if all required parameters have been set in the inspector
        if (jamWanted > 0 && jamType == JamTypes.None)
            Debug.LogError($"{this.name} wants jam but no JamType is set in the Inspector!");

        if (completed == null || outOfTime == null)
            Debug.LogError($"{this.name} is missing dialogue that needs to be assigned in the Inspector!");

        if (startNodeCompleted == "" || startNodeOutOfTime == "")
            Debug.LogWarning($"{this.name} is missing name(s) for the starting nodes for the Yarn programs.");

        if (runner == null)
            Debug.LogError($"{this.name} is missing a reference to the DialogueRunner component!");
    }

    void Start()
    {
        // Add Yarn programs to DialogueRunner
        runner.Add(completed);
        runner.Add(outOfTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (!finished)
            {
                PlayerControl player = other.gameObject.GetComponent<PlayerControl>();

                if (!player.presentHeld || breadWanted > player.breadHeld || (jamWanted > 0 && jamWanted > player.jamHeld[(int)jamType]))
                {
                    runner.StartDialogue("MissingItems");
                }
                else if (Timer.TimerInstance.TimeLeft() <= 0)
                {
                    finished = true;
                    runner.StartDialogue(startNodeOutOfTime);
                }
                else
                {
                    player.breadHeld -= breadWanted;
                    player.presentHeld = false;
                    if (jamType != JamTypes.None)
                        player.jamHeld[(int)jamType] -= jamWanted;

                    finished = true;
                    runner.StartDialogue(startNodeCompleted);
                }
            }
        }
    }
}