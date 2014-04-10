using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;


	void Start()
	{
		enabled = false;
		EventManager.Subscribe(OnEvent);
	}


	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			enabled = true;
			break;
		case EventManager.EVENT_BALL_EXIT:
			enabled = false;
			break;
		}
	}
	
	
	void Update () {
		print ("start updating score");
		displayScore = GameManager.instance.score;
		guiText.text = displayScore + "/" + Level.instance.minimumScore;		
	}
}
