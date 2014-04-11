using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public float spinningSpeed = 10f;
			
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward,spinningSpeed*Time.deltaTime);
	}
}
