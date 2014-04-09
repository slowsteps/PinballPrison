using UnityEngine;
using System.Collections;

public class MagnetSpawnPoint : MonoBehaviour {

	public static MagnetSpawnPoint currentMagnet;
	public bool isStartPoint = false;


	public void Awake()
	{
		if (isStartPoint) currentMagnet = this;
		EventManager.Subscribe(OnEvent);
	}
	
	

	public void OnTriggerEnter2D (Collider2D ball)
	{
		
		if (ball.tag == "ball") 
		{
			ball.rigidbody2D.gravityScale = 0f;
			ball.rigidbody2D.velocity = Vector3.zero;
			ball.rigidbody2D.angularVelocity = 0f;
			iTween.MoveTo(ball.gameObject,iTween.Hash("position",transform.position,"time",1f,"easetype",iTween.EaseType.easeOutElastic));
			currentMagnet = this;
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
