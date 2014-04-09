using UnityEngine;
using System.Collections;

public class ScrollCamera : MonoBehaviour {

	public static ScrollCamera instance;
	public GameObject lookatTarget = null;
	private Vector3 targetPos = Vector3.zero;
	public enum scrollDirections {none,horizontal,vertical,both};
	public scrollDirections scrollDirection = scrollDirections.horizontal;
	
	
	void Start () 
	{
		instance = this;
		scrollDirection = (scrollDirections) Level.instance.scrollDirection;
		print (scrollDirection);
	}

	public void SetTarget(GameObject inTarget)
	{
		lookatTarget = inTarget;
	}

	void Update () 
	{
		if (scrollDirection == scrollDirections.horizontal)
		{
			targetPos.x = lookatTarget.transform.position.x;
			targetPos.y = transform.position.y;
			targetPos.z = transform.position.z;
		}
		if (scrollDirection == scrollDirections.vertical)
		{
			targetPos.x = transform.position.x;
			targetPos.y = lookatTarget.transform.position.y + 1;
			targetPos.z = transform.position.z;
		}
		
		transform.position = Vector3.Slerp(transform.position,targetPos,2f*Time.deltaTime);
		
//		if (Mathf.Abs(targetPos.x - transform.position.x) > 2)
//		{
//			transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime);
//		}
	}
}
