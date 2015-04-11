using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TimeDisplay : MonoBehaviour {

	private float allowedTime = 0;
	private float curTime = 0;
	private Text TimeLeftLabel;


	void Awake()
	{
		TimeLeftLabel = gameObject.GetComponent<Text>();
		EventManager.Subscribe(OnEvent);
	}

	void InitTimeDisplay()
	{
		if (Level.instance.hasMaxTime)
		{
			allowedTime = Level.instance.allowedTime;
			curTime = allowedTime;
			InvokeRepeating("UpdateTimeDisplay",1f,1f);
		}
		else 
		{
			TimeLeftLabel.text = "no timelimit";
			//gameObject.SetActive(false);
		}
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.GOALS_OK_BUTTON_CLICKED:
			InitTimeDisplay();
			break;
		case EventManager.EVENT_BALL_EXIT:
			CancelInvoke("UpdateTimeDisplay");
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			CancelInvoke("UpdateTimeDisplay");
			break;
		case EventManager.EVENT_BALL_DEATH:
			CancelInvoke("UpdateTimeDisplay");
			InitTimeDisplay();
			break;
		case EventManager.HAMBURGER_BUTTON_CLICKED:
			CancelInvoke("UpdateTimeDisplay");
			break;	
		case EventManager.RESUME_BUTTON_CLICKED:
			InvokeRepeating("UpdateTimeDisplay",1f,1f);
			break;		
			//TODO QA with Invoke in Pause menu
		}
	}
	
	
	public void UpdateTimeDisplay() {
		curTime = curTime - 1;
		if (curTime > 0)
		{
			//TODO move declaration 
			TimeSpan ts = TimeSpan.FromSeconds(curTime);
			TimeLeftLabel.text = "Time left: " + ts.Minutes + ":" + ts.Seconds;
		}
		else
		{
			TimeLeftLabel.text = "0 secs";
			CancelInvoke("UpdateTimeDisplay");
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_TIME);
		}
	}
	
	
}
