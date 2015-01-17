using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour {


	
	void Start () 
	{
		if (particleSystem) particleSystem.renderer.sortingLayerName = "Effects";
	}
	
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (particleSystem)
		{
			particleSystem.time = 0f;
			particleSystem.Play();
		}
		ball.gameObject.SetActive(false);
		Invoke("DelayedEvent",2f);
		SoundManager.instance.PlaySound("SwordWhoosh1_SFX");
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
