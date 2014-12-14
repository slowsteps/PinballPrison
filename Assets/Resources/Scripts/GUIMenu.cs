﻿using UnityEngine;
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
			gameObject.SetActive(false);
			//iTween.MoveTo(gameObject,iTween.Hash("x",-16,"time",1f,"easetype",iTween.EaseType.easeInBack));
			break;
		case EventManager.EVENT_MESSAGE_OK:
			Show();
			break;
		}
	}
	
	private void Show()
	{
		gameObject.SetActive(true);
		//iTween.MoveTo(gameObject,iTween.Hash("x",origPos.x,"time",1f,"easetype",iTween.EaseType.easeOutCubic));
		EventManager.fireEvent(EventManager.EVENT_MENU_SHOW);
	}
	
}
