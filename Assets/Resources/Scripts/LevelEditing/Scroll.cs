using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public float scrollSpeed = 0.5f;
	private Vector3 origPos;
	private Vector3 newPos;

	// Use this for initialization
	void Start () {
		origPos = transform.position;
		newPos = origPos;
	}
	
	// Update is called once per frame
	void Update () {
		newPos.y = scrollSpeed*(ScrollCamera.instance.transform.position - ScrollCamera.instance.origPos).y;
		transform.position = newPos;
	}
}
