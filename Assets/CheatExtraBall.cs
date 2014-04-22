using UnityEngine;
using System.Collections;

public class CheatExtraBall : MonoBehaviour {

	void OnMouseDown()
	{
		GameManager.instance.balls++;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
	}
}
