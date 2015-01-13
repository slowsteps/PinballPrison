using UnityEngine;
using System.Collections;

public class SettingsPanel : MonoBehaviour {

	// Use this for initialization
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
			//gameObject.SetActive(true);
			break;
		case EventManager.SETTINGS_BUTTON_CLICKED:
			gameObject.SetActive(true);
			break;

		}
	}
	

 
}
