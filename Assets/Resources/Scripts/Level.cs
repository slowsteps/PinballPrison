using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public static Level instance;
	public enum scrollDirections {none,horizontal,vertical,both};
	public scrollDirections scrollDirection = scrollDirections.horizontal;	
		
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
