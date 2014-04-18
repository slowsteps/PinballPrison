using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {


	private Vector3 origPos = Vector3.zero;
	private Vector2 catapultForce = Vector2.zero;
	public GameObject mainCam = null;
	public GameObject cursor = null;
	public float clickRadius = 0.5f;
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
		EventManager.Subscribe(OnEvent);
		origPos = transform.position;
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
			return;
		}
		
		clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
	
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

	
	
	public void OnUp()
	{
		
		if (selectedBall == this)
		{
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
