using UnityEngine;
using System.Collections;

//IN GAME HUD - SCORE etc.
public class GUILevelHUD : MonoBehaviour {
	
	public static GUILevelHUD instance;
	
	void Awake()
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			Hide();
			break;
		case EventManager.EVENT_LEVEL_START:
			Show();
			break;
		case EventManager.EVENT_MENU_SHOW:
			Hide();
			break;
		}
	}
	
	private void Show()
	{
		gameObject.SetActive(true);
	}
	
	private void Hide()
	{
		gameObject.SetActive(false);
	}
	
}

