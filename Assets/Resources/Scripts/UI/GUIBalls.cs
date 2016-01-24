using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIBalls : MonoBehaviour {

	private Text ballLabel;
	

	// Use this for initialization
	void Start () 
	{
		ballLabel = gameObject.GetComponent<Text>();
		EventManager.Subscribe(OnEvent);
	}
	
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			UpdateLabel();
			break;
		case EventManager.EVENT_BALLS_UPDATED:
			UpdateLabel();
			break;
		case EventManager.EVENT_EXTRA_BALL:
			UpdateLabel();
			break;
		}
	}
	
	
	private void UpdateLabel()
	{
		//print ("updating Balls label");
		ballLabel.text = "You have " + GameManager.instance.balls + " balls ";
	}	
	
}
