using UnityEngine;
using System.Collections;

public class ScrollCamera : MonoBehaviour {

	public static ScrollCamera instance;
	public GameObject lookatTarget = null;
	private Vector3 targetPos = Vector3.zero;
	public Vector3 origPos;
	
	
	void Start () 
	{
		origPos = transform.position;
		instance = this;
		EventManager.Subscribe(OnEvent);
		enabled = false;
		camera.enabled = false;
		#if UNITY_EDITOR
		camera.orthographicSize = 5.68f;
		#endif
		#if UNITY_IPHONE && !UNITY_EDITOR
		camera.orthographicSize = Screen.height/200f;
		Debug.Log("in iphone");
		#endif
		Debug.Log("ortho size: " + camera.orthographicSize);
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
			camera.enabled = true;
			break;
		}
	}


	void Update () 
	{	
		targetPos.x = transform.position.x;
		targetPos.y = lookatTarget.transform.position.y + 1;
		targetPos.z = transform.position.z;
	
		transform.position = Vector3.Slerp(transform.position,targetPos,3f*Time.deltaTime);
	}
}
