using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public float bumpForce = 0f;
	public int bumpScore = 1;
	public Color notActivatedColor = Color.white;
	public Color activatedColor = Color.red;
	public bool isActivated = false;
	public bool isToggle = false;
	public Gate gate;
	
	
	void Start()
	{
		gate.AddToTargets(this);
	}
	
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		GameManager.instance.AddToScore(bumpScore);
		
		if (isToggle) 
		{
			isActivated = !isActivated;
			if (isActivated) gameObject.GetComponent<SpriteRenderer>().color = activatedColor;
			else gameObject.GetComponent<SpriteRenderer>().color = notActivatedColor;
		}
		else 
		{
			isActivated = true;
			gameObject.GetComponent<SpriteRenderer>().color = activatedColor;
		}
		gate.ReportTargetHit(this);
	}
}
