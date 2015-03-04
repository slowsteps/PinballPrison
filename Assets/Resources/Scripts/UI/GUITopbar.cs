using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUITopbar : MonoBehaviour {
	
	
	void Start () {
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			break;
		case EventManager.EVENT_ENDOFLEVEL_APPEARED:
			gameObject.SetActive(false);
			break;
		}
	}
	
}


