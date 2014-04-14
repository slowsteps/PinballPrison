﻿using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int scoreValue = 100;

	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			GameManager.instance.AddToScore(scoreValue);
			//gameObject.SetActive(false);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Destroy(collider2D);
			if (particleSystem)
			{
				particleSystem.time = 0f;
				particleSystem.Play();
			}
		}
	}


}
