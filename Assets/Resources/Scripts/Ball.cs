﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {


	private Vector3 origPos = Vector3.zero;
	private Vector3 origScale;
	private Vector2 catapultForce = Vector2.zero;
	public GameObject mainCam = null;
	public GameObject cursor = null;
	public GameObject cursorMouseUp;
	public Vector3 clickPos = Vector3.zero;
	private int isPullMode = 1;
	public static Ball selectedBall = null;
	public static Ball instance = null;
	public bool isCaptured = false;
	private bool isFirstClick = true;
	public float currentGravityScale = 1f;
	

	void Start () 
	{
		enabled = false;
		instance = this;
		tag = "ball";
		cursor.SetActive(false);
		cursorMouseUp.SetActive(false);
		EventManager.Subscribe(OnEvent);
		origPos = transform.position;
		origScale = transform.localScale;
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			Init();
			break;
		case EventManager.EVENT_BALL_DEATH:
			Init();
			break;
		case EventManager.EVENT_BALL_EXIT:
			print ("level complete");
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
			
		}
	}
	
//	public void OnCollisionEnter2D (Collision2D inColl)
//	{
//		print ("ball collides with: " + inColl.collider.name);
//		Time.timeScale = 0.1f;
//		Debug.DrawRay(inColl.contacts[0].point,inColl.contacts[0].normal,Color.green,10f);
//	}
	
	private void Init() 
	{
		transform.position = origPos;
		if (MagnetSpawnPoint.currentSavePoint) transform.position = MagnetSpawnPoint.currentSavePoint.transform.position;
		else if (MagnetSpawnPoint.startPointMagnet) transform.position = MagnetSpawnPoint.startPointMagnet.transform.position;
		gameObject.SetActive(true);
		rigidbody2D.isKinematic = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
		//iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
		enabled = true;
	}
	
	void Update () 
	{
		if (isFirstClick)
		{
			isFirstClick = false;
			return;
		}
		clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
		{
			if (gameObject.GetComponent<iTween>()) 
			{
				print ("killing tween on ball");
				Destroy(gameObject.GetComponent<iTween>());
				transform.localScale = origScale;
			}
			//clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			OnHitZoneDown();
		}
	
		if (Input.GetMouseButton(0))
		{
			//clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			OnHitZoneDown();
			DrawCursor();										
		}

		if (Input.GetMouseButtonUp(0))
		{
			OnHitZoneUp();
		}		
		
	}

	private void DrawCursor()
	{
		
		if (selectedBall == this)
		{ 		
			cursor.transform.position = new Vector3(clickPos.x,clickPos.y,0);
			
			Vector3 startPos = transform.position;
			startPos.z=-1;
			Vector3 endPos = cursor.transform.position;
			endPos.z=-1;
			
			Vector3 radius = endPos - startPos;
			radius = Vector3.ClampMagnitude(radius,1.5f);
			endPos = startPos + radius;
			//cursor.GetComponent<LineRenderer>().SetWidth(0.15f/radius.magnitude,0f);
			cursor.GetComponent<LineRenderer>().SetPosition(0,startPos);
			cursor.GetComponent<LineRenderer>().SetPosition(1,endPos);

		}
	}
	
	
	//Called from BallHitZone, larger radius than ball
	public void OnHitZoneDown()
	{
		
		cursor.transform.position = new Vector3(clickPos.x,clickPos.y,0);
		
		Vector3 startPos = transform.position;
		startPos.z=-1;
		Vector3 endPos = cursor.transform.position;
		endPos.z=-1;
		
		Vector3 radius = endPos - startPos;
	
		if (radius.magnitude < 4)		
		{
			selectedBall = this;
			ScrollCamera.instance.SetTarget(gameObject);
			cursor.SetActive(true);
		}
	}

	
	
	public void OnHitZoneUp()
	{
		if (selectedBall == this)
		{
			cursorMouseUp.SetActive(true);
			cursorMouseUp.transform.position = new Vector3(clickPos.x,clickPos.y,0);
			
			rigidbody2D.isKinematic = false;
			selectedBall = null;
			rigidbody2D.gravityScale = currentGravityScale;
			catapultForce = 1000f*(transform.position - cursor.transform.position)*isPullMode;
			if (catapultForce.magnitude > 1000f) catapultForce = 1000f*catapultForce.normalized;
			rigidbody2D.AddForce(catapultForce);
			cursor.SetActive(false);
		}
	}

}
