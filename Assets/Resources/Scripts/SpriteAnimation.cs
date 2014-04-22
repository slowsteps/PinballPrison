using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("Cycle",1f,1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void Cycle()
	{
		//gameObject.GetComponent<SpriteRenderer>().
	}
	
}
