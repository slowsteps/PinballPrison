
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
		if (startPointMagnet)  print("duplicate isStartPoint Magnet : " + name);
		
		enabled = false;
		if (isStartPoint) {
			startPointMagnet = this;
		}
		EventManager.Subscribe(OnEvent);
	}
	
	

	public void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "ball") 
		{
			currentMagnet = this;
			if (isSafePoint) currentSavePoint = this;
			Ball.instance.OnMagnet();
			SoundManager.instance.PlaySound("Magnet_SFX");
		}
	}
	
	public void OnTriggerLeave2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			currentMagnet = null;
		}
	}

	void OnDisable()
	{
		if (currentMagnet == this && Ball.instance) 
		{
			if (Ball.instance.GetComponent<Rigidbody2D>().gravityScale == 0f) Ball.instance.GetComponent<Rigidbody2D>().gravityScale = Ball.instance.currentGravityScale;
		}
	}

	//TODO do this everywere!
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);	
		//make sure currentSavePoint does not persist when switching to another level
		currentSavePoint = null;
			
	}

	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			enabled = true;
			break;
		case EventManager.EVENT_BALL_EXIT:
			if (isStartPoint) currentMagnet = this;
			break;
		}
	}
	
}
