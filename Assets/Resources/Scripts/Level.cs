using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public static Level instance;
	public bool hasMinScore = true;
	public bool hasMaxShots = false;
	public bool hasMaxTime = false;
	public bool hasCollectables = false;
	
	public int requiredScore = 10;
	public int allowedShots = 0;
	public float allowedTime = 0;
	public int requiredCollectables = 0;
			
	void Start () {
		
		if (!GameManager.instance)
		{
			Application.LoadLevelAdditive("Main");
		}
		
		if (!Settings.hasPlayerClicked)
		{
			GameObject.Destroy(gameObject);
			gameObject.SetActive(false);
			
		}
		
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
