using UnityEngine;
using System.Collections;

public class BallsLeft : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_BALLS_UPDATED:
			UpdateText();
			break;
		}
	}
	
	private void UpdateText()
	{
		guiText.text = GameManager.instance.balls + " balls left";
	}
	
	
}
