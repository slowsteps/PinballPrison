using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	
	void Awake () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		ball.gameObject.SetActive(false);
		particleSystem.time = 0f;
		particleSystem.Play();
		iTween.PunchScale(gameObject,new Vector3(0.6f,0.6f,0.6f),1f);
		Invoke("DelayedEvent",2f);	
		SoundManager.instance.PlaySound("ExitEnter_SFX");
	}
	
	private void DelayedEvent()
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_EXIT);
		SoundManager.instance.PlaySound("LevelSucces_SFX");
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			if (Level.instance.hasMinScore || Level.instance.hasCollectables) gameObject.SetActive(false);
			else gameObject.SetActive(true);
			break;
		case EventManager.EVENT_MINIMUMSCORE_REACHED:
			Show();
			break;
		case EventManager.EVENT_ALL_COLLECTABLES_FOUND:
			Show();
			break;
		}
	}
	
	private void Show()
	{
		gameObject.SetActive(true);
		iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),1f);
		SoundManager.instance.PlaySound("ExitAppear_SFX");
		EventManager.fireEvent(EventManager.EVENT_EXIT_VISIBLE);
	}
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
		CancelInvoke();
		iTween.Stop();
	}
	
}
