﻿using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {

	public buttonEnum myButton = buttonEnum.LEVEL;
	public enum buttonEnum {LEVEL,START,MENU};
	public int levelNumber=1;

	
	
	public void OnMouseDown()
	{
		switch(myButton)
		{
		case buttonEnum.LEVEL:
			LoadLevel(levelNumber);
			break;
		case buttonEnum.MENU:
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
			break;
		}
	
	}

	private void LoadLevel(int inLevelNum)
	{
		if (GameManager.instance.lives > 0)
		{
			Instantiate(Resources.Load("Prefabs/Level"+inLevelNum+"_Prefab"));
		}
		else print ("out of lives");
	}
		
				
}
