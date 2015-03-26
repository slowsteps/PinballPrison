using UnityEngine;
using System.Collections;


public class TimerSwitch : MonoBehaviour {



	public float startDelay = 0f;
	public float onTime = 3f;
	public float offTime = 3f;
	public bool hasScaleTween = true;
	private Vector2 origPos;
	private bool isFirstrun = true;
	

	void Start () 
	{
		origPos = transform.position;
		StartCoroutine(Animate());								
	}
	
	
	void Destroy () 
	{
		StopCoroutine(Animate());
		iTween.Stop();
	}

	
	IEnumerator Animate()
	{
		for(;;)
		{
			if (isFirstrun)
			{
				yield return new WaitForSeconds(startDelay);
				isFirstrun = false;
			}
			transform.Translate(100f,0,0);
			yield return new WaitForSeconds(offTime);
			transform.position = origPos;
			if (hasScaleTween) iTween.PunchScale(gameObject,new Vector3(0.6f,0.6f,0.6f),1f);
			yield return new WaitForSeconds(onTime);
		}
	}
	
	
	
}
