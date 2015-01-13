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
		case EventManager.EVENT_QUIT:
			gameObject.SetActive(true);
			break;
		case EventManager.SETTINGS_BUTTON_CLICKED:
			gameObject.SetActive(false);
			break;
			
		}
	}
}
