using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
[DisallowMultipleComponent]

public class Timer : MonoBehaviour
{
	[Tooltip("Initial amount of time in seconds.")]
	[SerializeField] private float startingTime = 600;
	[Tooltip("Amount of time remaining in seconds.")]
	[SerializeField] private float timer;

	public static Timer TimerInstance {get; private set;}

	private TMP_Text timerText;

    void Awake()
    {
    	timer = startingTime;
    	timerText = GetComponent<TMP_Text>();
    	TimerInstance = this;
    }

    void Update()
    {
    	if (timer <= 0)
    		timer = 0;

    	int minutes = (int) (timer/60);
        int seconds = (int) (timer%60);
        timerText.text = seconds < 10 ? $"Time: {minutes}:0{seconds}" : $"Time: {minutes}:{seconds}";

        if (timer <= 0)
        {	
        	this.enabled = false;
        	return;
        }

        timer -= Time.deltaTime;
    }

    public float TimeLeft()
    {
    	return timer;
    }
}
