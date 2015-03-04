using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TickerTapeScroller : MonoBehaviour {
		
	public float speed;
	private RectTransform ScrollingRect;
	private Vector2 OrigAnchorPos;
	private float savedTime = 0;
	private float scrollDistance;
	private Text ScrollText = null;
	

	
	public void Start() 
	{
		
		ScrollingRect = gameObject.GetComponent<RectTransform>();
		EventManager.Subscribe(OnEvent);
		ScrollText = gameObject.GetComponent<Text>();
		OrigAnchorPos = ScrollingRect.anchoredPosition;
		enabled = false;
	}
	
	
	
	public void OnEvent(string customEvent)
	{
		
		switch(customEvent)
		{
		case EventManager.GOALS_OK_BUTTON_CLICKED:
			ScrollText.text = Level.instance.LongDescription;	
			savedTime = Time.time;
			enabled = true;
			break;
		case EventManager.EVENT_BALL_EXIT:
			ScrollText.text = "";
			enabled = false;
			break;
			
		}
	}
	
	public void Update() 
	{
		scrollDistance = -speed*(Time.time - savedTime);
		if (ScrollingRect.anchoredPosition.x < - (2*Screen.width + ScrollingRect.rect.width)) savedTime = Time.time;
		ScrollingRect.anchoredPosition = new Vector2(scrollDistance,OrigAnchorPos.y);
	}
	
}

