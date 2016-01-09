using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {
	
	
	public float bumpForce = 400f;
	public int bumpScore = 1;
	public float scaleEffectAmp = 0.2f;
	public float scaleEffectTime = 0.5f;
	private bool isBumping = true;
	
	
	public void Start()
	{
		
		EventManager.Subscribe(OnEvent);
		
	}
	
		
	public void OnCollisionExit2D(Collision2D inColl)
	{
		if (isBumping)
		{	
			inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
			GameManager.instance.AddToScore(bumpScore,gameObject);
			SoundManager.instance.PlaySound("Select_SFX");
			
			if (!gameObject.GetComponent<iTween>())
			{
				iTween.PunchScale(gameObject,iTween.Hash("amount",scaleEffectAmp * Vector3.one,"time",scaleEffectTime));
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