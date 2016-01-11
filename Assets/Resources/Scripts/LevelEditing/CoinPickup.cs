using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinPickup : MonoBehaviour {

	public int scoreValue = 100;
	public string feedbackText = "Coin";
	public bool isCollected = false;
	public static List<CoinPickup> coinList;

	public void Start()
	{
		if (coinList == null) coinList = new List<CoinPickup>();
		coinList.Add(this);
	}


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
			isCollected = true;
			
			if (coinList.TrueForAll(coin => coin.isCollected)) EventManager.fireEvent(EventManager.EVENT_ALL_COINS_COLLECTED);
			
		}
	}
	
	public void OnDestroy()
	{
		coinList.Clear();
	}

	
}
