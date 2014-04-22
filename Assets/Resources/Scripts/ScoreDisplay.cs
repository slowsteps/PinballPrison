using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;
	public static ScoreDisplay instance;


	void Start()
	{
		instance = this;
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
		}
	}
	
	
	public void UpdateScoreDisplay(int inScore) {
		
		if (Level.instance.hasMinScore) guiText.text = inScore + "/" + Level.instance.requiredScore;		
		else guiText.text = inScore.ToString();
	}
}
