using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//this is the main menu with the level buttons, logo etc.

public class GUIMenu : MonoBehaviour {

	public static GUIMenu instance;
	


	void Awake()
	{
		instance = this;
		print("Awake Menu");
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(true);
		GridLayoutGroup grid = gameObject.GetComponentInChildren <GridLayoutGroup>();
		grid.enabled = false;
		grid.enabled = true;
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_QUIT:
			Show();
			break;
		case EventManager.EVENT_GAME_START:
			break;
		case EventManager.EVENT_LEVEL_START:
			Hide();
			break;
		case EventManager.EVENT_MESSAGE_OK:
			Show();
			break;
		case EventManager.LEVEL_BUTTON_CLICKED:
			//Hide();
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
