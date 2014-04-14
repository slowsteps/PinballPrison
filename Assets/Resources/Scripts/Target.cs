using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public float bumpForce = 0f;
	public int scoreValue = 1;
	public Color notActivatedColor = Color.white;
	public Color activatedColor = Color.red;
	[HideInInspector]
	public bool isActivated = false;
	public bool isToggle = false;
	public bool isLight = false;
	public TargetGroupEffect targetGroupEffect;
	private Sprite targetUp;
	public Sprite targetDown;
	
	void Awake()
	{
		EventManager.Subscribe(OnEvent);
		targetUp = gameObject.GetComponent<SpriteRenderer>().sprite;
		if (isLight) collider2D.isTrigger = true;
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
			if (targetGroupEffect) targetGroupEffect.AddTarget(this);
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
			collider2D.isTrigger = true;
		}
		if (targetGroupEffect) targetGroupEffect.ReportTargetHit(this);
	
	}


	public void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}

}
