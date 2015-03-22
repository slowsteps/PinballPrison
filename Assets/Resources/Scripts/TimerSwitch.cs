using UnityEngine;
using System.Collections;


public class TimerSwitch : MonoBehaviour {



	public float delay;
	public float duration = 1f;
	public bool willStartInvisible = false;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Toggle",delay,duration);
		if(willStartInvisible) gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Destroy () 
	{
		CancelInvoke("Toggle");
	}
	
	private void Toggle()
	{
		gameObject.SetActive(!gameObject.activeSelf);
	}
	
}
