using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Ball : MonoBehaviour {


	//private Vector3 origPos = Vector3.zero;
	private Vector2 catapultForce = Vector2.zero;
	public float tiltToleranceTime = 2f;
	public float MinForce = 200f;
	public float MaxForce = 1000f;
	public GameObject mainCam = null;
	public GameObject cursor = null;
	public float clickRadius = 0.5f;
	private Vector3 clickPos = Vector3.zero;
	private Vector3 startPos  = Vector3.zero;
	private int isPullMode = 1;
	public static Ball selectedBall = null;
	public static Ball instance = null;
	public bool isCaptured = false;
	public bool isFirstClick = true;
	public float currentGravityScale = 1f;
	public bool isDetectingTaps = true;
	private List<float> tiltClicks;
	private bool isTilt = false;
	
	
	
	

	void Start () 
	{
		enabled = false;
		instance = this;
		tag = "ball";
			
		cursor.SetActive(false);
		EventManager.Subscribe(OnEvent);
		//origPos = transform.position;
		gameObject.SetActive(false);
		tiltClicks = new List<float>();
		tiltClicks.Add(Time.timeSinceLevelLoad);
		tiltClicks.Add(Time.timeSinceLevelLoad);
		tiltClicks.Add(Time.timeSinceLevelLoad);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.GOALS_OK_BUTTON_CLICKED:
			Init();
			break;
		case EventManager.EVENT_BALL_DEATH:
			Init();
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_SHOTS:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_TIME:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_TILT_START:
			isTilt = true;
			break;
		case EventManager.EVENT_TILT_END:
			isTilt = false;
			break;
		case EventManager.HAMBURGER_BUTTON_CLICKED:
			Pause();
			break;
		case EventManager.RESUME_BUTTON_CLICKED:
			Resume();
			break;
					
		}
	}
	

	private void Pause()
	{
		isFirstClick = true;
		enabled = false;
	}
	
	private void Resume()
	{
		isFirstClick = false;
		enabled = true;
		
	}
	
	private void Init() 
	{
	
		if (MagnetSpawnPoint.currentSavePoint) transform.position = MagnetSpawnPoint.currentSavePoint.transform.position;
		else if (MagnetSpawnPoint.startPointMagnet) transform.position = MagnetSpawnPoint.startPointMagnet.transform.position;
		gameObject.SetActive(true);
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0f;
		if (!gameObject.GetComponent<iTween>()) iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
		enabled = true;
	}
	
	public void OnMagnet()
	{
		GetComponent<Rigidbody2D>().gravityScale = 0f;
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		GetComponent<Rigidbody2D>().angularVelocity = 0f;
		Transform target = MagnetSpawnPoint.currentMagnet.transform;
		iTween.MoveTo(this.gameObject,iTween.Hash("name","magnet","position",target.position,"time",1f,"easetype",iTween.EaseType.easeOutElastic));
	}
	
	
	void Update () 
	{
		//FirstClick is a click in the menu
		if (isFirstClick)
		{
			isFirstClick = false;
			return;
		}
		
		clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if (Input.GetMouseButtonDown(0))
		{
			SoundManager.instance.PlaySound("BallSelect_SFX");
		}
		
		
	
		if (!isTilt && Input.GetMouseButton(0) )
		{
			//check for touch sliding off the screen
			if (IsInsideScreen()) 
			{
				//select ball and draw rubber band when closeby
				OnDown();									
			}
			else 
			{
				DeselectBall();
			}
		}
		else 
		{
			//valid onscreen mouseup
			if (IsInsideScreen()) OnUp();
			//swipe off screen
			else DeselectBall(); 
			
			//if (isEnergyChargeEnabled) LeftChargeBar.RechargeLeftChargeBar();
		}
			
			
			
		
	}
	
	
	private bool IsInsideScreen()
	{
		if (Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width - 1 && Input.mousePosition.y > 17 && Input.mousePosition.y < Screen.height - 1) return true;
		else return false;
	}

	private void DeselectBall()
	{
		selectedBall = null;
		cursor.SetActive(false);
		
	}

	private void DrawCursor()
	{
		
		
		
		if (selectedBall == this)
		{
			
			cursor.transform.position = new Vector3(clickPos.x,clickPos.y,0);
			
			
			startPos = transform.position;
			startPos.z=-3;
			
			
			Vector3 endPos = cursor.transform.position;
			endPos.z=-3;
			
			Vector3 radius = endPos - startPos;
			radius = Vector3.ClampMagnitude(radius,1.5f);
			endPos = startPos + radius;
			
			
			cursor.GetComponent<LineRenderer>().SetPosition(0,startPos);
			cursor.GetComponent<LineRenderer>().SetPosition(1,endPos);


		}
		else
		{
			//Debug.Log("Selectball = " + selectedBall);
		}
	}
	
	
	//Called from BallHitZone, larger radius than ball
	public void OnDown()
	{
		
		
		cursor.transform.position = new Vector3(clickPos.x,clickPos.y,0);
		
		Vector3 startPos = transform.position;
		startPos.z=-1;
		Vector3 endPos = cursor.transform.position;
		endPos.z=-1;
		
		Vector3 radius = endPos - startPos;
	
			
		if (radius.magnitude < clickRadius)			
		{
			selectedBall = this;
			ScrollCamera.instance.SetTarget(gameObject);
			cursor.SetActive(true);
		}
		
		DrawCursor();
		
		
		
	}

	
	
	//plunger, shot
	public void OnUp()
	{
	
		
		if (selectedBall == this)
		{
			//stop magnet ball capture animation, because it prevents 
			
			iTween.StopByName("magnet");
			GetComponent<Rigidbody2D>().isKinematic = false;
			selectedBall = null;
			GetComponent<Rigidbody2D>().gravityScale = currentGravityScale;
			
			
			catapultForce = 1000f*(transform.position - cursor.transform.position)*isPullMode;
			
			//don't allow really small or very big shots
			catapultForce = Mathf.Clamp(catapultForce.magnitude,MinForce,MaxForce)*catapultForce.normalized;
			
			GetComponent<Rigidbody2D>().AddForce(catapultForce);
			cursor.SetActive(false);
			GameManager.instance.shotsPlayed++;
			EventManager.fireEvent(EventManager.EVENT_BALL_SHOT);
			SoundManager.instance.PlaySound("BallRelease_SFX");
			Time.timeScale = 1f;	
			
			//Title when clicking too fast
			
			tiltClicks[2] = tiltClicks[1];
			tiltClicks[1] = tiltClicks[0];
			tiltClicks[0] = Time.timeSinceLevelLoad;
			
			if ( (tiltClicks[0] - tiltClicks[2]) < tiltToleranceTime ) EventManager.fireEvent(EventManager.EVENT_TILT_START);
			
			
		}
		
		
	}

	




}
