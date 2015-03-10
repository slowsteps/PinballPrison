using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StandUpTarget : MonoBehaviour {

	public int hits = 0;
	public GameObject[] lights;
	public GameObject result;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		
		
		if (hits<lights.Length) 
		{
			lights[hits].GetComponent<IControlledByOther>().Activate();
			hits++;
		}
		
		if (hits==lights.Length) result.GetComponent<IControlledByOther>().Activate();
		
	}
}
