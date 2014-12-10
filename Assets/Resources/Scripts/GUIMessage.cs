using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMessage : MonoBehaviour {

	public static GUIMessage instance;
	public Text textField;

	void Start () {
		textField = GetComponent<Text>();
		instance = this;
		//gameObject.SetActive(false);
	}
	

	public void SetText(string inText)
	{
		print ("SetText " + inText);
		gameObject.SetActive(true);
		textField.text = inText;
	}


	public void ZOnMouseDown()
	{
		
		EventManager.fireEvent(EventManager.EVENT_MESSAGE_OK);
		gameObject.SetActive(false);
	}


}
