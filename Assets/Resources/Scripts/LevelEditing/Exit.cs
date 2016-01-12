﻿using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	private bool isEnabled = true;
	
	
	
	void Awake () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (isEnabled)
		{
			ball.gameObject.SetActive(false);
			GetComponent<ParticleSystem>().time = 0f;
			GetComponent<ParticleSystem>().Play();
			iTween.PunchScale(gameObject,new Vector3(0.6f,0.6f,0.6f),1f);
			EventManager.fireEvent(EventManager.EVENT_BALL_EXIT_ENTERED);
			Invoke("DelayedEvent",2f);	
			SoundManager.instance.PlaySound("ExitEnter_SFX");
		}
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
		case EventManager.EVENT_TILT_START:
			Hide();
			break;	
		case EventManager.EVENT_TILT_END:
			Show();
			break;	
		}
	}
	
	private void Hide()
	{
		isEnabled = false;
		gameObject.SetActive(false);
	}
			
	private void Show()
	{
		isEnabled = true;
		gameObject.SetActive(true);
		iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),1f);
		SoundManager.instance.PlaySound("ExitAppear_SFX");
	}
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
		CancelInvoke();
		//iTween.Stop();
	}
	
}
