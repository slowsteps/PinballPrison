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
		}
	}
}
