using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {


	public float bumpForce = 400f;
	public int bumpScore = 1;
	public float scaleEffectAmp = 0.2f;
	public float scaleEffectTime = 0.5f;
		
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		GameManager.instance.AddToScore(bumpScore);
		if (!gameObject.GetComponent<iTween>())
		{
			Vector3 targetPos = (Vector3)inColl.contacts[0].normal;
			iTween.PunchPosition(gameObject,iTween.Hash("amount",-scaleEffectAmp * targetPos,"time",scaleEffectTime,"space",Space.World));
			
		}
	}
	
}
