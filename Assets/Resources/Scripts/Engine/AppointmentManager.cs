using UnityEngine;
using System.Collections;
using System;

public class AppointmentManager : MonoBehaviour {

	[Header("Manages countdown for extra ball")]
	public float duration = 30f;
	private float savedTimeInSeconds;
	public float timeRemaining;
	public bool isCountingDown = false;
	public static AppointmentManager instance;

	// Use this for initialization
	void Awake () 
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_OUT_OF_BALLS:
			startCountdown();
			break;			
		}
	}
	
	private void startCountdown()
	{
		savedTimeInSeconds = Time.realtimeSinceStartup;
		isCountingDown = true;
	}
	
	void Update()
	{
		if (isCountingDown)
		{
			timeRemaining = duration - (Time.realtimeSinceStartup - savedTimeInSeconds);
			if (timeRemaining < 0) GrantExtraBall();
		}
	}
	
	private void GrantExtraBall() 
	{
		if (GameManager.instance.balls == 0) GameManager.instance.AddBall();
		isCountingDown = false;
	}
	
	
	
}
