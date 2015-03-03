using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour {


	
	void Start () 
	{
		if (GetComponent<ParticleSystem>()) GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Effects";
	}
	
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (GetComponent<ParticleSystem>())
		{
			GetComponent<ParticleSystem>().time = 0f;
			GetComponent<ParticleSystem>().Play();
		}
		ball.gameObject.SetActive(false);
		Invoke("DelayedEvent",2f);
		SoundManager.instance.PlaySound("Danger_SFX");
	}
	
	private void DelayedEvent()
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_DEATH);
	}
	
	public void OnDestroy()
	{
		Debug.Log(" destroyed");
		CancelInvoke();
	}
	
}
