using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TickerTapeScroller : MonoBehaviour {
		
	public float speed;
	private RectTransform ScrollingRect;
	private RectTransform OrigRect;
	private Vector2 OrigAnchorPos;
	private float savedTime = 0;
	public float scrollDistance;
	private Text ScrollText = null;
	

	
	public void Start() 
	{
		savedTime = Time.time;
		ScrollingRect = gameObject.GetComponent<RectTransform>();
		OrigRect = ScrollingRect;
		EventManager.Subscribe(OnEvent);
		ScrollText = gameObject.GetComponent<Text>();
		OrigAnchorPos = ScrollingRect.anchoredPosition;
	}
	
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			ScrollText.text = Level.instance.LongDescription;	
			break;
		case EventManager.EVENT_BALL_DEATH:
			
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			
			break;
		case EventManager.EVENT_OUT_OF_SHOTS:
			
			break;
		case EventManager.EVENT_OUT_OF_TIME:
			
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

