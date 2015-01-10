using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplierFeedback : MonoBehaviour {

	private Text Label;

	// Use this for initialization
	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		Label = gameObject.GetComponent<Text>();
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			break;
		case EventManager.EVENT_SCORE_MULTIPLIER:
			Label.text = GameManager.instance.ScoreMultiplier + "X Multiplier active";
			gameObject.SetActive(true);
			break;				
		case EventManager.EVENT_SCORE_MULTIPLIER_END:
			print("mult end");
			Label.text = "";
			gameObject.SetActive(false);
			break;				
			
		}
	}

	

}
