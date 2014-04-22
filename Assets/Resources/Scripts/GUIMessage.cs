using UnityEngine;
using System.Collections;

public class GUIMessage : MonoBehaviour {

	public static GUIMessage instance;
	public GameObject textField;

	void Start () {
		instance = this;
		gameObject.SetActive(false);
	}
	

	public void SetText(string inText)
	{
		gameObject.SetActive(true);
		textField.guiText.text = inText;
	}


	void OnMouseDown()
	{
		
		EventManager.fireEvent(EventManager.EVENT_MESSAGE_OK);
		gameObject.SetActive(false);
	}


}
