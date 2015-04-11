using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	

	public void OnHamburgerButton()
	{
		EventManager.fireEvent(EventManager.HAMBURGER_BUTTON_CLICKED);
		SoundManager.instance.PlaySound("Select_SFX");
	}
	
						
}
