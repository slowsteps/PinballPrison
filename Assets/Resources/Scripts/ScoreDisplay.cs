using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;
	public static ScoreDisplay instance;


	void Awake()
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
			UpdateScoreDisplay();
			break;
		}
	}
	
	
	public void UpdateScoreDisplay() {
		print ("UpdateScoreDisplay");
		if (Level.instance.hasMinScore) guiText.text = GameManager.instance.score + "/" + Level.instance.requiredScore;		
		else guiText.text = GameManager.instance.score.ToString();
	}
}
