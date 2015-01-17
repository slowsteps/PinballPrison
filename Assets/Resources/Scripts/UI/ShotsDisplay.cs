using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotsDisplay : MonoBehaviour {

	private Text ShotsLeftText;

	void Awake()
	{
		ShotsLeftText = GetComponent<Text>();
		EventManager.Subscribe(OnEvent);
		//gameObject.SetActive(false);
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			if (Level.instance.hasMaxShots)
			{
				gameObject.SetActive(true);
				UpdateShotsDisplay();
			}
			else
			{
				gameObject.SetActive(false);
			}
			break;
		case EventManager.EVENT_BALL_SHOT:
			UpdateShotsDisplay();
			break;	
		}
	}
	
	
	public void UpdateShotsDisplay() {
		ShotsLeftText.text = "Shots: " + GameManager.instance.shotsPlayed + "/" + Level.instance.allowedShots;		
	}
}
