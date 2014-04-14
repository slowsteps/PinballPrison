using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public static Level instance;
	public int minimumScore = 10;
			
	void Start () {
		
		if (!Settings.hasPlayerClicked)
		{
			GameObject.Destroy(gameObject);
			gameObject.SetActive(false);
			
		}
		
		instance = this;
		EventManager.Subscribe(OnEvent);
		
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_OUT_OF_BALLS:
			GameObject.Destroy(gameObject,0f);
			break;
		case EventManager.EVENT_BALL_EXIT:
			GameObject.Destroy(gameObject,0f);
			break;
		}
	}
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}
	
}
