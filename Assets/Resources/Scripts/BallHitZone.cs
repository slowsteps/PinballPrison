using UnityEngine;
using System.Collections;

public class BallHitZone : MonoBehaviour {


	public Ball myBall;

	// Update is called once per frame
	void Update () {
		transform.position = myBall.transform.position;
	}
	
	void OnMouseDown()
	{
		print ("down");
		myBall.OnHitZoneDown();
	}
	
	
	void OnMouseUp()
	{
		myBall.OnHitZoneUp();
	}
	
	
}
