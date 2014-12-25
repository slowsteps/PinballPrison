using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMessage : MonoBehaviour {

	public static GUIMessage instance;
	public Text textField;

	void Start () {
		instance = this;
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_MESSAGE_OK:
			gameObject.SetActive(false);
			break;
		}
	}
	


	public void SetText(string inText)
	{
		print ("SetText " + inText);
		gameObject.SetActive(true);
		textField.text = inText;
	}


}
