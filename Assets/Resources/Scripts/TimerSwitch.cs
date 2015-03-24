using UnityEngine;
using System.Collections;


public class TimerSwitch : MonoBehaviour {



	public float startDelay = 0f;
	public float onTime = 3f;
	public float offTime = 3f;
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
			yield return new WaitForSeconds(onTime);
		}
	}
	
	
	
}
