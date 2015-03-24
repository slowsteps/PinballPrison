
using UnityEngine;
using System.Collections;
using UnityEditor;

public class MagnetSpawnPoint : MonoBehaviour {

	public static MagnetSpawnPoint currentMagnet;
	public static MagnetSpawnPoint currentSavePoint;
	public static MagnetSpawnPoint startPointMagnet;
	public bool isStartPoint = false;
	public bool isSafePoint = false;

	public void Awake()
	{
		if (startPointMagnet)  print("duplicate isStartPoint Magnet : " + name);
		//if (startPointMagnet && isStartPoint) EditorUtility.DisplayDialog("Error","Multiple startpoints: "+ name,"ok");
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

	void OnDestroy()
	{
		//enabled = false;
		//iTween.Destroy(gameObject);
		EventManager.UnSubscribe(OnEvent);
		
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
