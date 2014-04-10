using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	void Start()
	{
		EventManager.Subscribe(OnEvent);
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_BALL_EXIT:
			gameObject.SetActive(true);
			break;
		}
	}
	
}
