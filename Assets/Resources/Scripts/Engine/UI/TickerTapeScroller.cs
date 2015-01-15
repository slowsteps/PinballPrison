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
		savedTime = Time.time;
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
		case EventManager.EVENT_LEVEL_START:
			ScrollText.text = Level.instance.LongDescription;	
			break;
		case EventManager.GOALS_OK_BUTTON_CLICKED:
			enabled = true;
			break;
		case EventManager.EVENT_MENU_SHOW:
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
