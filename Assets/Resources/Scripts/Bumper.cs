using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {


	public float bumpForce = 400f;
	public int bumpScore = 1;
	
	
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		GameManager.instance.AddToScore(bumpScore);
	}
	
}
