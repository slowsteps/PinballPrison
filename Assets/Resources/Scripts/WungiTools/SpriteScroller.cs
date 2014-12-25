using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteScroller : MonoBehaviour {
		
	public float speed;
	private RectTransform ScrollingRect;
	private RectTransform OrigRect;
	private float savedTime = 0;
	private float scrollDistance;
	
	public void Start() 
	{
		savedTime = Time.time;
		ScrollingRect = gameObject.GetComponent<RectTransform>();
		OrigRect = ScrollingRect;
	}
	
	
	
	public void Update() 
	{
		scrollDistance = speed*(Time.time - savedTime) - 1f;
		if (Mathf.Abs(scrollDistance) > 5) savedTime = Time.time;
		ScrollingRect.pivot = new Vector2(scrollDistance,OrigRect.pivot.y);
	}
	
}

