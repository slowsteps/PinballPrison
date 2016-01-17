using UnityEngine;
using System.Collections;

public class FreeBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnFreeBall()
	{
		GameManager.instance.AddBall();
	}
}
