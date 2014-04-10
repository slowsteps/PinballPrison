using UnityEngine;
using System.Collections;

public class ScrollCamera : MonoBehaviour {

	public static ScrollCamera instance;
	public GameObject lookatTarget = null;
	private Vector3 targetPos = Vector3.zero;
	
	
	void Start () 
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
		enabled = false;
		camera.enabled = false;
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
	
		transform.position = Vector3.Slerp(transform.position,targetPos,2f*Time.deltaTime);
	}
}
