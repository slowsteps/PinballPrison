using UnityEngine;
using System.Collections;

public class ScrollCamera : MonoBehaviour {

	public static ScrollCamera instance;
	public GameObject lookatTarget = null;
	private Vector3 targetPos = Vector3.zero;
	public Vector3 origPos;
	private Vector2 shakeOffset;
	
	
	void Start () 
	{
		origPos = transform.position;
		instance = this;
		EventManager.Subscribe(OnEvent);
		enabled = false;
		//GetComponent<Camera>().enabled = false;
		#if UNITY_EDITOR
		GetComponent<Camera>().orthographicSize = 5.68f;
		#endif
		#if UNITY_IPHONE && !UNITY_EDITOR
		//camera.orthographicSize = Screen.height/200f;
		//Debug.Log("in iphone");
		#endif
		shakeOffset = new Vector2();
		
	}


	public void SetTarget(GameObject inTarget)
	{
		lookatTarget = inTarget;
	}


	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			enabled = true;
			GetComponent<Camera>().enabled = true;
			break;
		case EventManager.EVENT_TILT_START:
			StartCoroutine("Shake");
			break;
		case EventManager.EVENT_TILT_END:
			StopCoroutine("Shake");
			shakeOffset = Vector2.zero;
			break;
		}
	}
	
	

	IEnumerator Shake()
	{
		for (;;)
		{
			shakeOffset = 1f*Random.insideUnitCircle;
			yield return new WaitForSeconds(Random.Range(0.03f,0.1f));
		}
	}

	void Update () 
	{	
		targetPos.x = origPos.x + shakeOffset.x;
		targetPos.y = lookatTarget.transform.position.y + 1 + shakeOffset.y;
		targetPos.z = transform.position.z;
	
		transform.position = Vector3.Slerp(transform.position,targetPos,3f*Time.deltaTime);
	}
}
