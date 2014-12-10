using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	
	
	
	public void OnMenuButton()
	{
		print ("OnMenuButton");
		EventManager.fireEvent(EventManager.EVENT_QUIT);
	}
	
	
}
