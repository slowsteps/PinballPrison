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
			ts = TimeSpan.FromSeconds(Mathf.RoundToInt(am.timeRemaining));
			label.text = string.Format("Time to extra ball: {0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
		}
		else gameObject.SetActive(false);
	}
}
