using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_EXIT);
	}
	
}
