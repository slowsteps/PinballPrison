using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	
	
	
	public void OnPauseButton()
	{
		EventManager.fireEvent(EventManager.PAUSE_BUTTON_CLICKED);
		SoundManager.instance.PlaySound("Select_SFX");
	}

	public void OnMenuButton()
	{
		EventManager.fireEvent(EventManager.EVENT_QUIT);
		SoundManager.instance.PlaySound("Select_SFX");
	}

	public void OnResumeButton()
	{
		EventManager.fireEvent(EventManager.RESUME_BUTTON_CLICKED);
		SoundManager.instance.PlaySound("Select_SFX");
	}
	
			
	

	public void OnSettingsButton()
	{
		EventManager.fireEvent(EventManager.SETTINGS_BUTTON_CLICKED);
		SoundManager.instance.PlaySound("Select_SFX");
	}
	
	
						
	
	public void OnCheatClick()
	{
		GameManager.instance.balls++;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
	}
	
						
}
