using UnityEngine;
using System.Collections;

public class ExtraBall : MonoBehaviour {

	public GameObject ballVisual;


	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		
		
		if (ball.tag == "ball") 
		{
			
			ballVisual.SetActive(false);
			
			//TODO expand eventmanager so that is can pass data in event objects)
			GameManager.instance.balls++;
			EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
			
			Destroy(GetComponent<Collider2D>());

			if (GetComponent<ParticleSystem>())
			{
				GetComponent<ParticleSystem>().time = 0f;
				GetComponent<ParticleSystem>().Play();
			}
			SoundManager.instance.PlaySound("PickUpCoin_SFX");
		}
	}
	
	
}
