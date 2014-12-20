using UnityEngine;
using System.Collections;

//this is the main menu with the level buttons, logo etc.

public class GUIMenu : MonoBehaviour {

	public static GUIMenu instance;
	private Vector3 origPos;


	void Awake()
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
		origPos = transform.position;
	}
	

	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_QUIT:
			Show();
			break;
		case EventManager.EVENT_GAME_START:
			Show();
			break;
		case EventManager.EVENT_LEVEL_START:
			Hide();
			break;
		case EventManager.EVENT_MESSAGE_OK:
			Show();
			break;
		}
	}
	
	private void Show()
	{
		gameObject.SetActive(true);
		EventManager.fireEvent(EventManager.EVENT_MENU_SHOW);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
	
	
			
}
