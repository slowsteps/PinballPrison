using UnityEngine;
using System.Collections;

public class GUITilt : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_TILT_START:
			gameObject.SetActive(true);
			break;
		case EventManager.EVENT_TILT_END:
			gameObject.SetActive(false);
			break;	
		case EventManager.EVENT_BALL_EXIT:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_LEVEL_FAILED:
			gameObject.SetActive(false);
			break;			
					
		}
	}
		



		
}
