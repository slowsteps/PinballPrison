using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {
	
	public Vector3 spinAngles;
	public Vector3 startAngle;
	public float delay=0;
		
	void Start()
	{
		transform.Rotate(startAngle);
		enabled = false;
		Invoke("startTurning",delay);
	}
	
	private void startTurning()
	{
		enabled = true;
	}	
	
	void Update () {
		transform.Rotate(1f*spinAngles*(Time.deltaTime));
	}



}
