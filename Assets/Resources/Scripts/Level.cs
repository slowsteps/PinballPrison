using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public static Level instance;
	public int minimumScore = 10;
			
	void Start () {
		
		instance = this;
		EventManager.Subscribe(OnEvent);
		EventManager.fireEvent(EventManager.EVENT_LEVEL_START);
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
