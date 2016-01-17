using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIBallEndOfLevel : MonoBehaviour {

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
		case EventManager.EVENT_BALLS_UPDATED:
			ballLabel.text = "You have " + GameManager.instance.balls + " balls ";
			break;
		}
	}
	
	
}
