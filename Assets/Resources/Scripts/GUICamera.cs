using UnityEngine;
using System.Collections;

public class GUICamera : MonoBehaviour {

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		camera.enabled = true;
	}


	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			camera.enabled = false;
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			camera.enabled = true;
			break;
		case EventManager.EVENT_BALL_EXIT:
			camera.enabled = true;
			break;	
		}
	}
}
