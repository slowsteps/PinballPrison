using UnityEngine;
using System.Collections;

public class ShotsDisplay : MonoBehaviour {

	void Start()
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			UpdateShotsDisplay();
			break;
		case EventManager.EVENT_BALL_EXIT:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_BALL_SHOT:
			UpdateShotsDisplay();
			break;
			
		}
	}
	
	
	public void UpdateShotsDisplay() {
		print (Level.instance);
		guiText.text = "Shots " + GameManager.instance.shotsPlayed + "/" + Level.instance.allowedShots;		
	}
}
