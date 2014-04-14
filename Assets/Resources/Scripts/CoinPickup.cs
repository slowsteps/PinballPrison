using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int scoreValue = 100;

	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
		GameManager.instance.AddToScore(scoreValue);
		gameObject.SetActive(false);
		}
	}
	
	private void Update()
	{
		transform.Rotate(Vector3.up,Time.deltaTime);
	}


}
