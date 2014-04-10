using UnityEngine;
using System.Collections;

public class LivesRefill : MonoBehaviour {

	private int remainingTime = 0 ;

	void Start () {
		EventManager.Subscribe(OnEvent);		
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			remainingTime = GameManager.instance.livesRefillTime;
			break;
		case EventManager.EVENT_MENU_SHOW:
			gameObject.SetActive(true);
			if (GameManager.instance.lives < 5)	StartCoroutine("UpdateText");
			break;
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(false);
			StopCoroutine("UpdateText");
			break;
		}
	}
	
	private IEnumerator UpdateText()
	{
		while (GameManager.instance.lives < 5)
		{
			remainingTime--;
			guiText.text = "Extra live in " + remainingTime + " secs";
			if (remainingTime == 0 ) remainingTime = 30;
			yield return new WaitForSeconds(1);
		}
	}
}
