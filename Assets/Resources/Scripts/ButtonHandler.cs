using UnityEngine;
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
	
	
}
