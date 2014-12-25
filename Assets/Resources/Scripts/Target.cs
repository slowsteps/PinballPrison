using UnityEngine;
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
	
	void Awake()
	{
		EventManager.Subscribe(OnEvent);
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
			foreach(TargetGroupEffect tg in targetGroupEffects) tg.AddTarget(this);
			break;
		}
	}
	
	public void OnTriggerEnter2D (Collider2D ball) 
	{
		HandleBallContact();
	}
	
	
	//if down sprites are not available, use color to indicate state
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		HandleBallContact();
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
			Destroy(gameObject.collider2D);
		}
		foreach(TargetGroupEffect tg in targetGroupEffects) tg.ReportTargetHit(this);
		
		if (isCollectable) EventManager.fireEvent(EventManager.EVENT_COLLECTABLE_FOUND);
		
	}


	public void Reset()
	{
		if (!isToggle)
		{
			isActivated = false;
			if (!targetDown) gameObject.GetComponent<SpriteRenderer>().color = notActivatedColor;
			else gameObject.GetComponent<SpriteRenderer>().sprite = targetUp;
			gameObject.AddComponent<BoxCollider2D>();
		}
	}

	public void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}

}
