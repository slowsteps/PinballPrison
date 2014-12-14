﻿using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	
	
	
	public void OnMenuButton()
	{
		print ("OnMenuButton");
		EventManager.fireEvent(EventManager.EVENT_QUIT);
	}
	
	public void OnMessageOkButton()
	{
		print ("OnMessageOkButton");
		EventManager.fireEvent(EventManager.EVENT_MESSAGE_OK);
	}
	
	public void OnStartLevelButton(int inLevelNum)
	{
		Settings.hasPlayerClicked = true;
		Debug.Log("OnStartLevelButton: " + inLevelNum);
		if (GameManager.instance.lives > 0)
		{
			Application.LoadLevelAdditive("Level"+inLevelNum);
		}
		else print ("out of lives");
	}
	
	
}
