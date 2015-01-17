using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject TopBar;
	public GameObject BottomBar;
	public GameObject Message;
	public GameObject Menu;
	public GameObject Settings;
	public GameObject Pause;
	public GameObject Goals;
	public static UIManager instance;

	
	void Awake () {
		instance = this;
		TopBar.SetActive(true);
		BottomBar.SetActive(true);
		Message.SetActive(false);
		Menu.SetActive(true);
		Settings.SetActive(true);
		Pause.SetActive(true);
		Goals.SetActive(false);
	}
	
	public void SetMessage(string inString)
	{
		Message.SetActive(true);
		Message.GetComponent<GUIMessage>().SetText(inString);
	}
	
			
}
