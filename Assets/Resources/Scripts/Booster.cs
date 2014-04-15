using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	
	public enum BoosterTypes {NO_GRAVITY,BULLET_TIME,FREEZE};
	public BoosterTypes boosterType = BoosterTypes.NO_GRAVITY;
	public float resetTime = 10f;
	
	void OnMouseDown()
	{
		switch(boosterType)
		{
		case BoosterTypes.NO_GRAVITY:
			Ball.instance.currentGravityScale = 0f;
			Ball.instance.rigidbody2D.gravityScale = 0f;
			Invoke("ResetNoGravity",resetTime);
			gameObject.SetActive(false);
			break;
		case BoosterTypes.FREEZE:
			Ball.instance.rigidbody2D.velocity = Vector2.zero;
			Ball.instance.rigidbody2D.angularVelocity = 0f;
			Ball.instance.rigidbody2D.gravityScale = 0f;
			Invoke("ResetFreeze",resetTime);
			gameObject.SetActive(false);
			break;
			
		}
	} 


	private void ResetNoGravity()
	{
		gameObject.SetActive(true);
		Ball.instance.currentGravityScale = 1f;
		Ball.instance.rigidbody2D.gravityScale = 1f;
	}

	private void ResetFreeze()
	{
		gameObject.SetActive(true);
		Ball.instance.rigidbody2D.gravityScale = Ball.instance.currentGravityScale;
	}
	

}
