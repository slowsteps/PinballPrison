using UnityEngine;
using System.Collections;

public class LivesDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.Subscribe(OnEvent);
		
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			gameObject.SetActive(true);
			UpdateText();
			break;
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			UpdateText();
			break;
		case EventManager.EVENT_LIVES_UPDATED:
			UpdateText();
			break;
		}
	}
	
	private void UpdateText()
	{
		guiText.text = GameManager.instance.lives + " Lives";
	}
}
