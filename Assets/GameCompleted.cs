using UnityEngine;
using System.Collections;

public class GameCompleted : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_GAME_COMPLETED:
			gameObject.SetActive(true);
			break;	
		case EventManager.EVENT_GAME_RESET:
			gameObject.SetActive(false);
			break;	
			
		}
	}
	
}
