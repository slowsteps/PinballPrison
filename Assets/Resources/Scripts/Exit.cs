using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {


	// Use this for initialization
	void Awake () {
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		ball.gameObject.SetActive(false);
		particleSystem.time = 0f;
		particleSystem.Play();
		iTween.PunchScale(gameObject,new Vector3(0.6f,0.6f,0.6f),1f);
		Invoke("DelayedEvent",2f);	
		TextFeedback.Display("Level complete!",gameObject);				
	}
	
	private void DelayedEvent()
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_EXIT);
		//gameObject.SetActive(false);
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			if (Level.instance.hasMinScore) gameObject.SetActive(false);
			else gameObject.SetActive(true);
			break;
		case EventManager.EVENT_MINIMUMSCORE_REACHED:
			gameObject.SetActive(true);
			iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),1f);
			break;
		}
	}
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}
	
}
