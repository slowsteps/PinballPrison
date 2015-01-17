using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFeedback : MonoBehaviour {

	private float lifeTime = 3f;
	private Text Label;
	private float savedTime = 0;

	void Awake () 
	{
		savedTime = Time.time;
		Label = gameObject.GetComponent<Text>();
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_EXIT_VISIBLE:
			SetText("Exit is now open");
			break;
		case EventManager.EVENT_LEVEL_START:
			SetText("Tap and drag the ball");
			break;
		case EventManager.EVENT_MINIMUMSCORE_REACHED:
			SetText("Target score reached");
			break;		
		}
	}
	
	
	// Update is called once per frame
	void Update () {		
		if ( (Time.time - savedTime) > lifeTime) 
		{
			SetText(""); 
			enabled = false;
		}
	}

	public void SetText(string inText)
	{
		savedTime = Time.time;
		Label.text = inText;
		enabled = true;
	}

}
