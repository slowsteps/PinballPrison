using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("y", .50, "easeType", "easeIn", "loopType", "none", "delay", .4));
	}
}

