using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public static Level instance;
	public int minimumScore = 10;
		
	void Start () {
		instance = this;
		print ("level instantiated");
		EventManager.fireEvent(EventManager.EVENT_LEVEL_START);
	}
	
}
