using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Ball : MonoBehaviour {


	private Vector3 origPos = Vector3.zero;
	private Vector2 catapultForce = Vector2.zero;
	public GameObject mainCam = null;
	public GameObject cursor = null;
	
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
	public ChargeBar LeftChargeBar;
	private Sprite SquareBall = null;
	private Sprite RoundBall = null;
	public bool isDetectingTaps = true;
	

	void Start () 
	{
		enabled = false;
		instance = this;
		tag = "ball";
			
		cursor.SetActive(false);
		EventManager.Subscribe(OnEvent);
		origPos = transform.position;
		gameObject.SetActive(false);
		//LeftChargeBar.gameObject.SetActive(false);
		SquareBall = Resources.Load <Sprite> ("2D/Square");
		RoundBall = gameObject.GetComponent<SpriteRenderer>().sprite;
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
		case EventManager.EVENT_CHARGE_DEPLETED:
			isDetectingTaps = false;
			break;
		}
	}
	

	
	private void Init() 
	{
		transform.position = origPos;
		if (MagnetSpawnPoint.currentSavePoint) transform.position = MagnetSpawnPoint.currentSavePoint.transform.position;
		else if (MagnetSpawnPoint.startPointMagnet) transform.position = MagnetSpawnPoint.startPointMagnet.transform.position;
		gameObject.SetActive(true);
//		rigidbody2D.isKinematic = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
		if (!gameObject.GetComponent<iTween>()) iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
		enabled = true;
	}
	
	public void OnMagnet()
	{
		rigidbody2D.gravityScale = 0f;
		rigidbody2D.velocity = Vector3.zero;
		rigidbody2D.angularVelocity = 0f;
		//rigidbody2D.isKinematic = true;
		Transform target = MagnetSpawnPoint.currentMagnet.transform;
		iTween.MoveTo(this.gameObject,iTween.Hash("name","magnet","position",target.position,"time",1f,"easetype",iTween.EaseType.easeOutElastic));
	}
	
	
	void Update () 
	{
		//FirstClick is a click in the menu
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
		
		
		
		
	
		if (LeftChargeBar.isDetectingTap && Input.GetMouseButton(0) )
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
//			LeftChargeBar.DecreaseLeftChargeBar();
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
			//stop magnet ball capture animation, because it prevents 
			//iTween.Stop();
			iTween.StopByName("magnet");
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
			//if (catapultForce.magnitude > 1000f) catapultForce = 1000f*catapultForce.normalized;
			catapultForce = 1000f*catapultForce.normalized;
			
			rigidbody2D.AddForce(catapultForce);
			cursor.SetActive(false);
			GameManager.instance.shotsPlayed++;
			EventManager.fireEvent(EventManager.EVENT_BALL_SHOT);
			
			Time.timeScale = 1f;	
			
			
			
		}
		
		
	}

	

	//Settings screen functions
	
	public void OnActivateFreeTap(bool inEnabled)
	{
		LeftChargeBar.gameObject.SetActive(inEnabled);
		isFreeTap = inEnabled;
		isSlowMotionEnabled = inEnabled;
	}
		

	public void OnActivateSquareBall(bool inEnabled)
	{
		if (inEnabled) gameObject.GetComponent<SpriteRenderer>().sprite = SquareBall;
		else gameObject.GetComponent<SpriteRenderer>().sprite = RoundBall;
	}


}
