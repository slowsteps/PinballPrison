using UnityEngine;
using System.Collections;

public class BallsLeft : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			UpdateText();
			break;
		case EventManager.EVENT_BALLS_UPDATED:
			UpdateText();
			break;
		case EventManager.EVENT_BALL_EXIT:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
		}
	}
	
	private void UpdateText()
	{
		guiText.text = GameManager.instance.balls + " Balls";
	}
	
	
}
