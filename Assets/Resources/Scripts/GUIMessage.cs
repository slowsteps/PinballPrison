using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMessage : MonoBehaviour {

	public static GUIMessage instance;
	public Text textField;

	void Start () {
		print ("GUIMessage " + this);
		instance = this;
		gameObject.SetActive(false);
	}
	

	public void SetText(string inText)
	{
		print ("SetText " + inText);
		gameObject.SetActive(true);
		textField.text = inText;
	}


}
