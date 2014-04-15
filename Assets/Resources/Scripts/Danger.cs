using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour {


	public static GameObject ImpactParticles  = null;

	// Use this for initialization
	void Start () {
		if (ImpactParticles == null) 
		{
			ImpactParticles = GameObject.Find("DangerImpactParticles");
			ImpactParticles.SetActive(false);
			ImpactParticles.particleSystem.renderer.sortingLayerName = "Effects";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		print ("danger impact");
		ImpactParticles.SetActive(true);
		ImpactParticles.particleSystem.time = 0f;
		ImpactParticles.particleSystem.Play();
		ImpactParticles.transform.position = ball.transform.position;
		ball.gameObject.SetActive(false);
		Invoke("DelayedEvent",2f);
	}
	
	private void DelayedEvent()
	{
		EventManager.fireEvent(EventManager.EVENT_BALL_DEATH);
	}
	
}
