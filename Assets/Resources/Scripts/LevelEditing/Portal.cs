using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {


	public int scoreValue = 100;
	public GameObject PortalExit = null;
	public float TravelTime = 0f;
	

	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			GameManager.instance.AddToScore(scoreValue,gameObject);
	
			iTween.PunchScale(gameObject,new Vector3(0.6f,0.6f,0.6f),1f);
			if (PortalExit) Ball.instance.gameObject.SetActive(false);
		
			Invoke("EmergeAtExit",TravelTime);
		
			if (GetComponent<ParticleSystem>())
			{
				GetComponent<ParticleSystem>().time = 0f;
				GetComponent<ParticleSystem>().Play();
			}
		}
	}
	
	private void EmergeAtExit()
	{
		Ball.instance.gameObject.SetActive(true);
		if (PortalExit) Ball.instance.transform.position = PortalExit.transform.position;
	}

	void OnDestroy()
	{
		iTween.Stop();
		CancelInvoke();
	}
			
}
