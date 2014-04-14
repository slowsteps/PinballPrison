using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public Vector3 spinAngles;
	public float delay=0;	
	
	void Start()
	{
		enabled = false;
		Invoke("startTurning",delay);
	}
		
	private void startTurning()
	{
		enabled = true;
	}	
		
	void Update () {
		transform.Rotate(60f*spinAngles*(Time.deltaTime));
	}
}
