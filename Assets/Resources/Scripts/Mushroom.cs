using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {
	
	
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
			iTween.PunchScale(gameObject,iTween.Hash("amount",scaleEffectAmp * Vector3.one,"time",scaleEffectTime));
		}
	}
	
}