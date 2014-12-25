using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour {


	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "Effects";
	}
	
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		particleSystem.time = 0f;
		particleSystem.Play();
		ball.gameObject.SetActive(false);
		Invoke("DelayedEvent",2f);
	}
	
	private void DelayedEvent()
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_DEATH);
	}
	
	public void OnDestroy()
	{
		Debug.Log(this + " destroyed");
		CancelInvoke();
	}
	
}
