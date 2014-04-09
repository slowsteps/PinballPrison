using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {


	public float bumpForce = 400f;
	
	
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		GameManager.instance.AddToScore(1);
	}
	
}
