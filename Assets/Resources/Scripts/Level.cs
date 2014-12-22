﻿using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public static Level instance;
	public bool hasMinScore = true;
	public bool hasMaxShots = false;
	public bool hasMaxTime = false;
	public bool hasCollectables = false;
	
	public int requiredScore = 0;
	public int allowedShots = 0;
	public float allowedTime = 0;
	public int requiredCollectables = 0;
			
	void Awake () {
		
		instance = this;
		
		//a level is open in the editor
		if (!GameManager.instance)
		{
			Application.LoadLevelAdditive("Main");
		}
		//TODO can't remember why this is needed
		if (!Settings.hasPlayerClicked)
		{
			GameObject.Destroy(gameObject);
			gameObject.SetActive(false);
		}
		
		
		EventManager.Subscribe(OnEvent);
		EventManager.fireEvent(EventManager.EVENT_LEVEL_START);
		
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_MENU_SHOW:
			iTween.Stop(); // kill all mushroom anims etc.
			GameObject.Destroy(gameObject);
			break;
		}
	}
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}
	
}
