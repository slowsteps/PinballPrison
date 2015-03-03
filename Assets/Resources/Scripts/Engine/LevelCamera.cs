using UnityEngine;
using System.Collections;

public class LevelCamera : MonoBehaviour {

	
	void Start () {
		EventManager.Subscribe(OnEvent);
		GetComponent<Camera>().enabled = false;
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			GetComponent<Camera>().enabled = true;
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			GetComponent<Camera>().enabled = false;
			break;
			
		}
	}
	
}
