using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesRefill : MonoBehaviour {

	private int curTime = 0 ;
	private int refillTime = 0;
	private Text LivesLabel;

	void Awake () 
	{
		LivesLabel = GetComponent<Text>();
		EventManager.Subscribe(OnEvent);		
	}
	
	void Start()
	{
		//if (GameManager.instance.lives < 5)	InvokeRepeating("UpdateText",1f,1f);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			refillTime = GameManager.instance.livesRefillTime;
			curTime = 0;
			UpdateText();
			break;
		case EventManager.EVENT_MENU_SHOW:
			gameObject.SetActive(true);
			if (GameManager.instance.lives < 5)	
			{
				InvokeRepeating("UpdateText",2f,1f);
			}
			break;
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(false);
			CancelInvoke("UpdateText");
			break;
		}
	}
	
	private void UpdateText()
	{
		//print ("updating lives refill");
		if (GameManager.instance.lives < 5)
		{
			curTime++;
			LivesLabel.text = "Extra live in " + (refillTime - curTime) + " secs";
			if (curTime == refillTime) 
			{ 
				GameManager.instance.lives++;
				EventManager.fireEvent(EventManager.EVENT_LIVES_UPDATED);
				curTime = 0;
			}
		}
		else
		{
			LivesLabel.text = "";
		}
	}
}
