using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIEndOfLevel : MonoBehaviour {
	
	public static GUIEndOfLevel instance;
	public Text textField;
	public GameObject SuccessHeader;
	public GameObject FailedHeader;
	
	void Start () {
		instance = this;
		EventManager.Subscribe(OnEvent);
		SuccessHeader.SetActive(false);
		FailedHeader.SetActive(false);
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_MESSAGE_OK:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_BALL_EXIT:
			gameObject.SetActive(true);
			SuccessHeader.SetActive(true);
			FailedHeader.SetActive(false);
			break;
		case EventManager.EVENT_LEVEL_FAILED:
			gameObject.SetActive(true);
			SuccessHeader.SetActive(false);
			FailedHeader.SetActive(true);
			SoundManager.instance.PlaySound("LevelFail_SFX");
			break;
			
						
		}
	}
	
	
	
	public void SetMessage(string inText)
	{
		//print ("SetText " + inText);
		gameObject.SetActive(true);
		textField.text = inText;
	}
	
	
}

