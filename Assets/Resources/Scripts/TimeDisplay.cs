using UnityEngine;
using System.Collections;

public class TimeDisplay : MonoBehaviour {

	private float allowedTime = 0;
	private float curTime = 0;


	void Awake()
	{
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
			guiText.text = curTime.ToString("0") +  " secs";
		}
		else
		{
			guiText.text = "0 secs";
			CancelInvoke("UpdateTimeDisplay");
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_TIME);
		}
	}
	
	
}
