using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	
	
	
	public void OnPauseButton()
	{
		EventManager.fireEvent(EventManager.PAUSE_BUTTON_CLICKED);
	}

	public void OnMenuButton()
	{
		EventManager.fireEvent(EventManager.EVENT_QUIT);
	}

	public void OnResumeButton()
	{
		EventManager.fireEvent(EventManager.RESUME_BUTTON_CLICKED);
	}
	
			
	public void OnMessageOkButton()
	{
		EventManager.fireEvent(EventManager.EVENT_MESSAGE_OK);
	}
	
	public void OnStartLevelButton(int inLevelNum)
	{
		Settings.hasPlayerClicked = true;
			if (Application.CanStreamedLevelBeLoaded("Level"+inLevelNum)) 
			{
				Application.LoadLevelAdditive("Level"+inLevelNum);
				EventManager.fireEvent(EventManager.LEVEL_BUTTON_CLICKED);
			}
			else print("can't load scene Level"+inLevelNum) ;
	}

	public void OnSettingsButton()
	{
		EventManager.fireEvent(EventManager.SETTINGS_BUTTON_CLICKED);
	}
	
	
						
	
	public void OnCheatClick()
	{
		GameManager.instance.balls++;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
	}
	
						
}
