using UnityEngine;
using System.Collections;

public class GUIPause : MonoBehaviour {

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);	
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			gameObject.SetActive(false);
			break;
		case EventManager.PAUSE_BUTTON_CLICKED:
			gameObject.SetActive(true);
			break;
		case EventManager.HAMBURGER_BUTTON_CLICKED:
			gameObject.SetActive(true);
			break;
		}
	}
	
	
	public void OnPauseButton()
	{
		EventManager.fireEvent(EventManager.PAUSE_BUTTON_CLICKED);
		SoundManager.instance.PlaySound("Select_SFX");
	}
	
	public void OnMenuButton()
	{
		EventManager.fireEvent(EventManager.EVENT_QUIT);
		SoundManager.instance.PlaySound("Select_SFX");
		gameObject.SetActive(false);
	}
	
	public void OnResumeButton()
	{
		EventManager.fireEvent(EventManager.RESUME_BUTTON_CLICKED);
		SoundManager.instance.PlaySound("Select_SFX");
		gameObject.SetActive(false);
	}
	
}
