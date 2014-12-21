using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TickerTape : MonoBehaviour {


	public string ScrollText = "Welcome to Pinball Prision - the best pinball game in the appstore - tap and drag the ball to get going!";
	public float DelayBeforeStart = 0;
	public float TickDuration = 0.2f;
	private string Padding = "***                                                                   ***"; 
	private Text TickerTapeText;
	private int TickerPosition = 0;
	
	void Start () {
	
		TickerTapeText = gameObject.GetComponent<Text>();
		TickerTapeText.text = "";
		//Padding = TickerTapeText
		ScrollText = Padding + ScrollText;
		
		EventManager.Subscribe(OnEvent);
	}
	
	
	private void UpdateTickerTape () {
		TickerTapeText.text = ScrollText.Substring(TickerPosition);	
		TickerPosition++;
		if (TickerPosition > ScrollText.Length) TickerPosition = 0;	
	}

	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			TickerPosition = 0;
			//TODO destroy invoke at gameover
			InvokeRepeating("UpdateTickerTape",DelayBeforeStart,TickDuration);
			break;
		}
	}


}
