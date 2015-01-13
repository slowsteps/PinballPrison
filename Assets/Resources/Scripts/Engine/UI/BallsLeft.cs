using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallsLeft : MonoBehaviour {

	private Text BallsLeftText;

	// Use this for initialization
	void Awake () {
		BallsLeftText = GetComponent<Text>();
		EventManager.Subscribe(OnEvent);
		//gameObject.SetActive(false);
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
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
		}
	}
	
	private void UpdateText()
	{
		BallsLeftText.text = "Balls left: " + GameManager.instance.balls;
	}
	
	
}
