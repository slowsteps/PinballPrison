using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GUIGoals : MonoBehaviour {

	public Text ScoreLabel;
	public Text ShotsLabel;
	public Text Timelabel;
	
	void Awake () 
	{
		EventManager.Subscribe(OnEvent);
		//gameObject.SetActive(true);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.LEVEL_BUTTON_CLICKED:
			//gameObject.SetActive(true);
			break;
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			if (Level.instance.hasMinScore) ScoreLabel.text = "Required score: " + Level.instance.requiredScore;
			if (Level.instance.hasMaxShots) ShotsLabel.text = "Allowed shots: " + Level.instance.allowedShots;
			if (Level.instance.hasMaxTime) 
			{	
				TimeSpan ts = TimeSpan.FromSeconds(Level.instance.allowedTime);
				Timelabel.text = "Allowed time: " + ts.Minutes + ":" + ts.Seconds;
			}
			break;
		}
	}
	
	public void OnOkButton()
	{
		EventManager.fireEvent(EventManager.GOALS_OK_BUTTON_CLICKED);
		gameObject.SetActive(false);
	}
	
	
	
	
}
