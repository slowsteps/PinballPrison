using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	
	
	
	public void OnMenuButton()
	{
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
		//Debug.Log("OnStartLevelButton: " + inLevelNum);
		if (GameManager.instance.lives > 0)
		{
			if (Application.CanStreamedLevelBeLoaded("Level"+inLevelNum)) 
			{
				Application.LoadLevelAdditive("Level"+inLevelNum);
				EventManager.fireEvent(EventManager.LEVEL_BUTTON_CLICKED);
			}
			else print("can't load scene Level"+inLevelNum) ;
		}
		else print ("out of lives");
	}
	
	
	public void OnCheatClick()
	{
		GameManager.instance.balls++;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
	}
	
						
}
