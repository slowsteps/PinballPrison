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
	public GameObject Tilt;
	public GameObject EndOfLevel;
	public static UIManager instance;

	
	void Start () 
	{
		instance = this;
		TopBar.SetActive(true);
		Message.SetActive(true);
		//Menu.SetActive(true);
		Pause.SetActive(true);
		Goals.SetActive(true);
		Tilt.SetActive(true);	
	}
	

	
			
}
