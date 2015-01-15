﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : MonoBehaviour {

	public float bumpForce = 0f;
	public int scoreValue = 1;
	public Color notActivatedColor = Color.white;
	public Color activatedColor = Color.red;
	[HideInInspector]
	public bool isActivated = false;
	public bool isToggle = false;
	public bool isLight = false;
	public bool isCollectable = false;
	public List<TargetGroupEffect> targetGroupEffects;
	private Sprite targetUp;
	public Sprite targetDown;
	public bool isDetecting = true;
	
	void Awake()
	{
		EventManager.Subscribe(OnEvent);
		//print ("subscribed to eventmanager: " + name);
		targetUp = gameObject.GetComponent<SpriteRenderer>().sprite;
		if (isLight) collider2D.isTrigger = true;
		//targetGroupEffects = new List<TargetGroupEffect>();
	}
	
	void Start()
	{
		if (!targetDown) gameObject.GetComponent<SpriteRenderer>().color = notActivatedColor;
	
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			foreach(TargetGroupEffect tg in targetGroupEffects) 
			{
				//print (this.name + "-tg: " + tg);
				if (tg) 
				{
					tg.AddTarget(this);
					//print ("adding target to gate " + this.name);
				}
				else Debug.Log ("WARNING target does not exist in targetGroupEffects in " + this.name);
			}
			break;
		}
	}
	
	public void OnTriggerEnter2D (Collider2D ball) 
	{
		if (isDetecting) HandleBallContact();
	}
	
	
	//if down sprites are not available, use color to indicate state
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		if (isDetecting) HandleBallContact();
	}

	private void HandleBallContact()
	{
		GameManager.instance.AddToScore(scoreValue);
		
		if (isToggle) 
		{
			isActivated = !isActivated;
			
			if (!targetDown) 
			{
				if (isActivated) gameObject.GetComponent<SpriteRenderer>().color = activatedColor;
				else gameObject.GetComponent<SpriteRenderer>().color = notActivatedColor;
			}
			else 
			{
				if (isActivated) gameObject.GetComponent<SpriteRenderer>().sprite = targetDown;
				else gameObject.GetComponent<SpriteRenderer>().sprite = targetUp;
			}
		}
		else 
		{
			isActivated = true;
			if (!targetDown) gameObject.GetComponent<SpriteRenderer>().color = activatedColor;
			else gameObject.GetComponent<SpriteRenderer>().sprite = targetDown;

			
			gameObject.collider2D.isTrigger = true;
			//ZZ Destroy(gameObject.collider2D);
			
		}
		foreach(TargetGroupEffect tg in targetGroupEffects) tg.ReportTargetHit(this);
		
		if (isCollectable) 
		{
			StopDetecting();
			print ("collectable found " + this.name);
			EventManager.fireEvent(EventManager.EVENT_COLLECTABLE_FOUND);
		}
		
	}


	public void StopDetecting()
	{
		isDetecting = false;
	}

	//TargetDown is a sprite image
	public void Reset()
	{
		//if (!isToggle)
		if (true)
		{
			isDetecting = true;
			isActivated = false;
			if (!targetDown) gameObject.GetComponent<SpriteRenderer>().color = notActivatedColor;
			else gameObject.GetComponent<SpriteRenderer>().sprite = targetUp;
			//make sure other gates have not already added a collider
			//TODO maybe use trigger instead of destroying and adding colliders?
			//if (!gameObject.collider2D) gameObject.AddComponent<BoxCollider2D>();
			
			if (isLight) collider2D.isTrigger = true;
			else collider2D.isTrigger = false;
			
			//print ("target reset=" + this.name);
		}
	}

	public void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}

}
