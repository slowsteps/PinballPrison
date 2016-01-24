using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GUIFreeBallAppointmentCountdown : MonoBehaviour {

	public Text label;
	private TimeSpan ts;
	private AppointmentManager am;

	// Use this for initialization
	void Start () 
	{
		am = AppointmentManager.instance;
	}
	
	void Update () 
	{
		if (am.isCountingDown)
		{
			gameObject.SetActive(true);
			
			label.text = "time remaining " + am.timeRemaining;
		}
		else gameObject.SetActive(false);
	}
}
