using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {

	public Vector2 bumpForce;

	public void OnTriggerStay2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			ball.rigidbody2D.AddForce(bumpForce);
		}
	}
}
