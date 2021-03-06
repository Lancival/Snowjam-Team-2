using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]

public class Timer : MonoBehaviour
{
	[Tooltip("Initial amount of time in seconds.")]
	[SerializeField] private float startingTime = 600;
	[Tooltip("Amount of time remaining in seconds.")]
	[SerializeField] private float timer;

	public static Timer TimerInstance {get; private set;}

	private TMP_Text minutesText;
	private TMP_Text secondsText;

    private bool active;

    void Awake()
    {
    	minutesText = transform.GetChild(0).GetComponent<TMP_Text>();
    	secondsText = transform.GetChild(1).GetComponent<TMP_Text>();
    	TimerInstance = this;

        ChangeTimer(Settings.TIMER_ACTIVE);
        Settings.onTimerActiveChanged.AddListener(ChangeTimer);
    }

    void OnDestroy()
    {
        Settings.onTimerActiveChanged.RemoveListener(ChangeTimer);
    }

    private void ChangeTimer(bool newActive)
    {
        active = newActive;
        minutesText.transform.parent.gameObject.SetActive(newActive);
        if (active)
            timer = startingTime;
    }

    void Update()
    {
        if (active)
        {
        	if (timer <= 0)
        		timer = 0;

            int seconds = (int) (timer%60);
            minutesText.text = ((int) (timer / 60)).ToString("D2");
            secondsText.text = ((int) (timer % 60)).ToString("D2");

            if (timer <= 0)
            {	
            	this.enabled = false;
            	return;
            }

            timer -= Time.deltaTime;
        }
    }

    public float TimeLeft()
    {
    	return timer;
    }
}
