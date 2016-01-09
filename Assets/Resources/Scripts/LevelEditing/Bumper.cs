using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {


	public float bumpForce = 400f;
	public int bumpScore = 1;
	public float scaleEffectAmp = 0.2f;
	public float scaleEffectTime = 0.5f;
	public bool isBumping = true;	
		
		
	public void Start()
	{
		EventManager.Subscribe(OnEvent);
	}	
		
	
	public void OnCollisionExit2D(Collision2D inColl)
	{
		if (isBumping)
		{
			GameManager.instance.AddToScore(bumpScore,gameObject);	
			inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
			
			if (!gameObject.GetComponent<iTween>())
			{
				Vector3 targetPos = (Vector3)inColl.contacts[0].normal;
				iTween.PunchPosition(gameObject,iTween.Hash("amount",-scaleEffectAmp * targetPos,"time",scaleEffectTime,"space",Space.World));
			}
			
		}
	}

		
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_TILT_START:
			isBumping = false;
			break;	
		case EventManager.EVENT_TILT_END:
			isBumping = true;
			break;	
		}
	}
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}
	
	
}
