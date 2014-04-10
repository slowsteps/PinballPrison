using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;


	void Start()
	{
		enabled = false;
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}


	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			enabled = true;
			break;
		case EventManager.EVENT_BALL_EXIT:
			enabled = false;
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
		}
	}
	
	
	void Update () {
		displayScore = GameManager.instance.score;
		guiText.text = displayScore + "/" + Level.instance.minimumScore;		
	}
}
