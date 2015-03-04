using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//this is the main menu with the level buttons, logo etc.

public class GUIMenu : MonoBehaviour {

	public static GUIMenu instance;
	public GameObject logo;
	
	

	void Start()
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
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
		case EventManager.EVENT_ENDOFLEVEL_DISAPPEARED:
			Show();
			break;
		case EventManager.LEVEL_BUTTON_CLICKED:
			//Hide();
			break;			
		}
	}
	
	
	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			logo.GetComponent<Animator>().Play("Logo2",-1,0f);
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
