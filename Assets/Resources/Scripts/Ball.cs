using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Ball : MonoBehaviour {


	private Vector3 origPos = Vector3.zero;
	private Vector2 catapultForce = Vector2.zero;
	public GameObject mainCam = null;
	public GameObject cursor = null;
	public GameObject AimGuidance = null;
	public float clickRadius = 0.5f;
	private Vector3 clickPos = Vector3.zero;
	private Vector3 OrigClickPos = Vector3.zero;
	private Vector3 startPos  = Vector3.zero;
	private Vector3 Shot = Vector3.zero;
	private int isPullMode = 1;
	public static Ball selectedBall = null;
	public static Ball instance = null;
	public bool isCaptured = false;
	private bool isFirstClick = true;
	public float currentGravityScale = 1f;
	public bool isFreeTap = true;
	public bool isSlowMotionEnabled = false;
	public bool isTapSpeedConstrained = false;
	public float Energy = 0;
	public float LowEnergyBarrier = 30f;
	private float EnergyDecay = 0.95f;
	private bool ShotIsAllowed = true;
	
	public Color32 SlowColor = Color.red;
	public Color32 FastColor = Color.green;
	public SpriteRenderer BallSpriteRenderer;
	
	

	void Start () 
	{
		enabled = false;
		instance = this;
		tag = "ball";
			
		cursor.SetActive(false);
		EventManager.Subscribe(OnEvent);
		origPos = transform.position;
		gameObject.SetActive(false);
		AimGuidance.SetActive(false);
		BallSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
		case EventManager.EVENT_OUT_OF_BALLS:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_SHOTS:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_TIME:
			gameObject.SetActive(false);
			break;
		}
	}
	

	
	private void Init() 
	{
		transform.position = origPos;
		if (MagnetSpawnPoint.currentSavePoint) transform.position = MagnetSpawnPoint.currentSavePoint.transform.position;
		else if (MagnetSpawnPoint.startPointMagnet) transform.position = MagnetSpawnPoint.startPointMagnet.transform.position;
		gameObject.SetActive(true);
		rigidbody2D.isKinematic = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
		if (!gameObject.GetComponent<iTween>()) iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
		enabled = true;
	}
	
	void Update () 
	{
		if (isFirstClick)
		{
			isFirstClick = false;
			//for free tap
			OrigClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return;
		}
		
		clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if (Input.GetMouseButtonDown(0))
		{
			OrigClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		
		//from settings screen
		if (isTapSpeedConstrained) UpdateEnergy();	
		if (isTapSpeedConstrained && !ShotIsAllowed )return;
	
		if (Input.GetMouseButton(0))
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
		}
			
			//from settings screen
			
		
	}
	
	private void UpdateEnergy()
	{
		
		Energy = Energy + rigidbody2D.velocity.magnitude;
		Energy = Energy * EnergyDecay;
		if (Energy < LowEnergyBarrier) 
		{
			BallSpriteRenderer.color = SlowColor;
			ShotIsAllowed = true;
		}
		else 
		{
			BallSpriteRenderer.color = FastColor;
			ShotIsAllowed = false;
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
			
			if (isFreeTap) 
			{
				startPos = OrigClickPos;
			}
			else
			{
				startPos = transform.position;
			}
			
			startPos.z=-3;
			
			
			Vector3 endPos = cursor.transform.position;
			endPos.z=-3;
			
			Vector3 radius = endPos - startPos;
			radius = Vector3.ClampMagnitude(radius,1.5f);
			endPos = startPos + radius;
			
			if (isFreeTap)
			{
				Shot = endPos - startPos;
				startPos = transform.position;
				endPos = startPos + Shot;
			}
			
			
			cursor.GetComponent<LineRenderer>().SetPosition(0,startPos);
			cursor.GetComponent<LineRenderer>().SetPosition(1,endPos);

//			if (isFreeTap)
//			{
//				AimGuidance.SetActive(true);
//				AimGuidance.transform.position = transform.position;
//				AimGuidance.transform.LookAt(cursor.transform);
//				AimGuidance.transform.Rotate(Vector3.up,180);
//			}
		}
		else
		{
			//Debug.Log("Selectball = " + selectedBall);
		}
	}
	
	
	//Called from BallHitZone, larger radius than ball
	public void OnDown()
	{
		
		
		if (isFreeTap) 
		{
			cursor.transform.position = new Vector3(OrigClickPos.x,OrigClickPos.y,0);
		}
		else
		{
			cursor.transform.position = new Vector3(clickPos.x,clickPos.y,0);
		}
		
		Vector3 startPos = transform.position;
		startPos.z=-1;
		Vector3 endPos = cursor.transform.position;
		endPos.z=-1;
		
		Vector3 radius = endPos - startPos;
	
		//if (radius.magnitude < clickRadius)		
		if (isFreeTap || (radius.magnitude < clickRadius) )			
		{
			selectedBall = this;
			ScrollCamera.instance.SetTarget(gameObject);
			cursor.SetActive(true);
			if (isSlowMotionEnabled) Time.timeScale = 0.1f;
		}
		
		DrawCursor();
		
		
		
	}

	
	
	//plunger, shot
	public void OnUp()
	{
		
		if (selectedBall == this)
		{
			rigidbody2D.isKinematic = false;
			selectedBall = null;
			rigidbody2D.gravityScale = currentGravityScale;
			
			if (isFreeTap) 
			{
				catapultForce = 1000f * -Shot;
			}
			else
			{
				catapultForce = 1000f*(transform.position - cursor.transform.position)*isPullMode;
			}
			if (catapultForce.magnitude > 1000f) catapultForce = 1000f*catapultForce.normalized;
			rigidbody2D.AddForce(catapultForce);
			cursor.SetActive(false);
			GameManager.instance.shotsPlayed++;
			EventManager.fireEvent(EventManager.EVENT_BALL_SHOT);
			if (isSlowMotionEnabled) Time.timeScale = 0.1f;Time.timeScale = 1f;	
			if (isFreeTap) AimGuidance.SetActive(false);
		}
		
		
	}

	//Settings screen functions
	
	public void OnActivateFreeTap(bool inEnabled)
	{
		Debug.Log("OnActivateFreeTap " + inEnabled);
		isFreeTap = inEnabled;
	}
		
	public void OnActivateSlowmotion(bool inEnabled)
	{
		isSlowMotionEnabled = inEnabled;
	}
	
	public void OnActivateTapSpeedConstraint(bool inEnabled)
	{
		
		Debug.Log("isTapSpeedConstrained " + inEnabled);
		isTapSpeedConstrained = inEnabled;
	}
	


}
