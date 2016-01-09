using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour {


	
	void Start () 
	{
		//TODO this can now also be set manually in the property inspector
		if (GetComponent<ParticleSystem>()) GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Effects";
	}
	
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (GetComponent<ParticleSystem>())
		{
			GetComponent<ParticleSystem>().time = 0f;
			GetComponent<ParticleSystem>().Play();
		}
		//TODO this is dirty, should be encapsulated in Ball
		ball.gameObject.SetActive(false);
		Ball.instance.DeselectBall();
		
		Invoke("DelayedEvent",2f);
		SoundManager.instance.PlaySound("Danger_SFX");
	}
	
	private void DelayedEvent()
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_DEATH);
	}
	
	public void OnDestroy()
	{
		CancelInvoke();
	}
	
}
