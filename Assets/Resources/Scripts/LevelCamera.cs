using UnityEngine;
using System.Collections;

public class LevelCamera : MonoBehaviour {

	
	void Start () {
		EventManager.Subscribe(OnEvent);
		camera.enabled = false;
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			camera.enabled = true;
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			camera.enabled = false;
			break;
			
		}
	}
	
}
