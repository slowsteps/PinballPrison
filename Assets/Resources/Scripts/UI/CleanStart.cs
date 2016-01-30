using UnityEngine;
using System.Collections;

public class CleanStart : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_RESET:
			gameObject.SetActive(true);
			break;
		case EventManager.EVENT_GAME_START:
			gameObject.SetActive(true);
			//if (GameManager.instance.currentLevel == 1) gameObject.SetActive(true);
			break;
		}
	}
	
	public void OnContinueButton()
	{
		gameObject.SetActive(false);
	}
	
}
