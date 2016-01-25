using UnityEngine;
using System.Collections;
using System;

public class AppointmentManager : MonoBehaviour {

	[Header("Manages countdown for extra ball")]
	public double duration = 30;
	public DateTime savedDateTime;
	public TimeSpan timeRemaining;
	public bool isCountingDown = false;
	public static AppointmentManager instance;

	// Use this for initialization
	void Awake () 
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
		//picking up from last session.
		if(PlayerPrefs.HasKey("savedDateTime")) 
		{
			long temp = Convert.ToInt64(PlayerPrefs.GetString("savedDateTime"));
			savedDateTime = DateTime.FromBinary(temp);
			print("retreived savedDateTime: " + savedDateTime.Hour + ":"  + savedDateTime.Minute);
			isCountingDown = true;
		}
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_OUT_OF_BALLS:
			startCountdown();
			break;			
		}
	}
	
	private void startCountdown()
	{
		savedDateTime = DateTime.Now;
		
		isCountingDown = true;
		PlayerPrefs.SetString("savedDateTime",savedDateTime.ToBinary().ToString());
		print("storing savedTimeInSeconds: " + savedDateTime.Hour + ":"  + savedDateTime.Minute);
	}
	
	void Update()
	{
		if (isCountingDown)
		{
			timeRemaining = TimeSpan.FromSeconds( duration - ( DateTime.Now.Subtract(savedDateTime).TotalSeconds ));
			if (timeRemaining.TotalSeconds < 0) GrantExtraBall();
		}
	}
	
	private void GrantExtraBall() 
	{
		if (GameManager.instance.balls == 0) GameManager.instance.AddBall();
		isCountingDown = false;
		PlayerPrefs.DeleteKey("savedTimeInSeconds");
	}
	
	
	
}
