using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	public static GUIMenu instance;


	void Awake()
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
	}
	
	void Start() 
	{
		print ("start called");
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			Show();
			break;
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			Show();
			break;
		case EventManager.EVENT_BALL_EXIT:
			Show();
			break;
		}
	}
	
	private void Show()
	{
		gameObject.SetActive(true);
		EventManager.fireEvent(EventManager.EVENT_MENU_SHOW);
	}
	
}
