using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {


	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
		GameManager.instance.AddToScore(100);
		gameObject.SetActive(false);
		}
	}


}
