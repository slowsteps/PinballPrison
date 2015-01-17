using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour {

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);	
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.PAUSE_BUTTON_CLICKED:
			gameObject.SetActive(true);
			break;
		case EventManager.SETTINGS_BUTTON_CLICKED:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_QUIT:
			gameObject.SetActive(false);
			break;
		case EventManager.RESUME_BUTTON_CLICKED:
			gameObject.SetActive(false);
			break;
			
		}
	}
}
