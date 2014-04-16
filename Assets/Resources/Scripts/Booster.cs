using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	
	public enum BoosterTypes {NO_GRAVITY,BULLET_TIME,FREEZE};
	public BoosterTypes boosterType = BoosterTypes.NO_GRAVITY;
	public float resetTime = 10f;
	public Vector3 clickDeltaPos;	
	private bool isBoosterActive = false;
	private Vector3 origPos;
	
	void Start()
	{
		origPos = transform.position;
	}
	
	
	void OnMouseDown()
	{
		switch(boosterType)
		{
		case BoosterTypes.NO_GRAVITY:
			Ball.instance.currentGravityScale = 0f;
			Ball.instance.rigidbody2D.gravityScale = 0f;
			Time.timeScale = 0.25f;
			Invoke("ResetNoGravity",resetTime);
			break;
		case BoosterTypes.FREEZE:
			Ball.instance.rigidbody2D.velocity = Vector2.zero;
			Ball.instance.rigidbody2D.angularVelocity = 0f;
			Ball.instance.rigidbody2D.gravityScale = 0f;
			Invoke("ResetFreeze",resetTime);
			break;
		}
		
		if (!isBoosterActive) 
		{
			iTween.MoveTo(gameObject,iTween.Hash("name",name,"position",origPos + clickDeltaPos,"time",0.5f,"easetype",iTween.EaseType.easeInBack,"ignoretimescale",true));
			isBoosterActive = true;
		}
		
		
	} 


	private void ResetNoGravity()
	{
		Time.timeScale = 1f;
		Ball.instance.currentGravityScale = 1f;
		Ball.instance.rigidbody2D.gravityScale = 1f;
		ResetBooster();
	}

	private void ResetFreeze()
	{
		Ball.instance.rigidbody2D.gravityScale = Ball.instance.currentGravityScale;
		ResetBooster();
	}
	
	private void ResetBooster()
	{
		gameObject.SetActive(true);
		transform.position = origPos;
		iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),1f);
		isBoosterActive = false;
	}

}
