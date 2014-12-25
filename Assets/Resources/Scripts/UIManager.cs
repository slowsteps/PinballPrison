using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject TopBar;
	public GameObject BottomBar;
	public GameObject Message;
	public GameObject Menu;
	public GameObject Settings;
	public static UIManager instance;

	
	void Start () {
		instance = this;
		TopBar.SetActive(true);
		BottomBar.SetActive(true);
		Message.SetActive(false);
		Menu.SetActive(true);
		Settings.SetActive(false);
	}
	
	public void SetMessage(string inString)
	{
		Message.SetActive(true);
		Message.GetComponent<GUIMessage>().SetText(inString);
	}
	
			
}
