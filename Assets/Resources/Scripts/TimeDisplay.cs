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
			gameObject.SetActive(false);
		}
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			InitTimeDisplay();
			break;
		case EventManager.EVENT_BALL_EXIT:
			CancelInvoke("UpdateTimeDisplay");
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			CancelInvoke("UpdateTimeDisplay");
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_BALL_DEATH:
			CancelInvoke("UpdateTimeDisplay");
			InitTimeDisplay();
			break;
		}
	}
	
	
	public void UpdateTimeDisplay() {
		curTime = curTime - 1;
		if (curTime > 0)
		{
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
