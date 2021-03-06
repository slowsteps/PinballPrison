﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIExtraBall : MonoBehaviour {

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_EXTRA_BALL:
			gameObject.SetActive(true);
			Invoke("Hide",3f);
			break;
		}
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
		

}
