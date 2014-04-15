using UnityEngine;
using System.Collections;

public class MagnetSpawnPoint : MonoBehaviour {

	public static MagnetSpawnPoint currentMagnet;
	public static MagnetSpawnPoint currentSavePoint;
	public static MagnetSpawnPoint startPointMagnet;
	public bool isStartPoint = false;
	public bool isSafePoint = false;


	public void Awake()
	{
		if (isStartPoint) {
			startPointMagnet = this;
		}
		EventManager.Subscribe(OnEvent);
	}
	
	

	public void OnTriggerEnter2D (Collider2D ball)
	{
		
		if (!isStartPoint && ball.tag == "ball") 
		{
			ball.rigidbody2D.gravityScale = 0f;
			ball.rigidbody2D.velocity = Vector3.zero;
			ball.rigidbody2D.angularVelocity = 0f;
			ball.transform.position = transform.position;
			//iTween.MoveTo(ball.gameObject,iTween.Hash("position",transform.position,"time",1f,"easetype",iTween.EaseType.easeOutElastic));
			currentMagnet = this;
			if (isSafePoint) currentSavePoint = this;
		}
	}

	void OnDisable()
	{
		if (currentMagnet == this && Ball.instance) 
		{
			if (Ball.instance.rigidbody2D.gravityScale == 0f) Ball.instance.rigidbody2D.gravityScale = Ball.instance.currentGravityScale;
		}
	}

	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_BALL_EXIT:
			if (isStartPoint) currentMagnet = this;
			break;
		}
	}
	
}
