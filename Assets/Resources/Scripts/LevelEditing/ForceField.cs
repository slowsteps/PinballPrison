using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {

	public Vector2 bumpForce;
	public bool isLocal = false;

	public void OnTriggerStay2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			if(isLocal) ball.rigidbody2D.AddForce(transform.rotation * bumpForce);
			else ball.rigidbody2D.AddForce(bumpForce);
		}
	}
}
