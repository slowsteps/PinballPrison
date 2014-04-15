using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public Vector3 spinAngles;
	public float delay=0;	
	
	void Start()
	{
		enabled = false;
		Invoke("startTurning",delay);
		HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>();
		if (joint) joint.connectedAnchor = (Vector2) transform.position;
	}
		
	private void startTurning()
	{
		enabled = true;
	}	
		
	void Update () {
		transform.Rotate(60f*spinAngles*(Time.deltaTime));
	}
}
