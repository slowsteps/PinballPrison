using UnityEngine;
using System.Collections;

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
		case EventManager.EVENT_GAME_START:
			Show();
			break;
		case EventManager.EVENT_LEVEL_START:
			iTween.MoveTo(gameObject,iTween.Hash("x",8,"time",0.5f,"easetype",iTween.EaseType.easeInBack));
			//gameObject.SetActive(false);
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
		iTween.MoveTo(gameObject,iTween.Hash("x",origPos.x,"time",0.5f,"easetype",iTween.EaseType.easeOutBack));
		EventManager.fireEvent(EventManager.EVENT_MENU_SHOW);
	}
	
}
