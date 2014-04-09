﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {


	private Vector3 origPos = Vector3.zero;
	private Vector2 catapultForce = Vector2.zero;
	public GameObject mainCam = null;
	public static GameObject cursor = null;
	public Vector3 clickPos = Vector3.zero;
	private int isPullMode = 1;
	public static Ball selectedBall = null;
	public static Ball instance = null;
	public bool isCaptured = false;
	

	void Start () {
		instance = this;
		tag = "ball";
		if (cursor==null) 
		{
			cursor = GameObject.Find("Cursor");
			cursor.SetActive(false);
		}
		EventManager.Subscribe(OnEvent);
		origPos = transform.position;
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
			Init();
			break;
		}
	}
	
	
	private void Init() {
		gameObject.SetActive(true);
		rigidbody2D.isKinematic = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
		transform.position = origPos;
		rigidbody2D.isKinematic = false;
		if (MagnetSpawnPoint.currentMagnet) transform.position = MagnetSpawnPoint.currentMagnet.transform.position;
		iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
	}
	
	
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			DrawCursor();
													
		}
		
		if (Input.GetKeyDown(KeyCode.R)) isPullMode = -1*isPullMode;
				
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
			
			cursor.GetComponent<LineRenderer>().SetPosition(0,startPos);
			cursor.GetComponent<LineRenderer>().SetPosition(1,endPos);
		}
	}
	
	//BUG? ball and hitzone have collider conflicts, so both need to trigger mousedowns and ups.
	void OnMouseDown()
	{
		OnHitZoneDown();
	}

	void OnMouseUp()
	{
		OnHitZoneUp();
	}

	//Called from BallHitZone, larger radius than ball
	public void OnHitZoneDown()
	{
		selectedBall = this;
		ScrollCamera.instance.SetTarget(gameObject);
		cursor.SetActive(true);
	}

	
	
	public void OnHitZoneUp()
	{
		selectedBall = null;
		rigidbody2D.gravityScale = 1f;
		catapultForce = 1000f*(transform.position - cursor.transform.position)*isPullMode;
		if (catapultForce.magnitude > 1000f) catapultForce = 1000f*catapultForce.normalized;
		rigidbody2D.AddForce(catapultForce);
		cursor.SetActive(false);
	}

}
