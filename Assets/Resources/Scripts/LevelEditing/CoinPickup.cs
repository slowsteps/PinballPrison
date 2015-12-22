using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int scoreValue = 100;
	public string feedbackText = "Coin";

	public void OnTriggerEnter2D (Collider2D ball)
	{
		
		
		if (ball.tag == "ball") 
		{
			GameManager.instance.AddToScore(scoreValue,gameObject);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
