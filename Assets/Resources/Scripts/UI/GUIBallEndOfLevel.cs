using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIBallEndOfLevel : MonoBehaviour {

	private Text ballLabel;
	private int extraBallWaitDuration = 30;

	// Use this for initialization
	void Start () 
	{
		ballLabel = gameObject.GetComponent<Text>();
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_BALLS_UPDATED:
			ballLabel.text = "You have " + GameManager.instance.balls + " balls ";
			if (GameManager.instance.balls == 0) 
			{
				StartCoroutine(CountdownToExtraBall());
			}
			else StopCoroutine(CountdownToExtraBall());
			
			break;
		}
	}
	
	
	public IEnumerator CountdownToExtraBall()
	{
		while (extraBallWaitDuration > 0 )
		{
			ballLabel.text = extraBallWaitDuration + " seconds to extra ball ";
			extraBallWaitDuration--;
			if (extraBallWaitDuration == 0) GameManager.instance.AddBall();
			yield return new WaitForSeconds(1);
		}
		
	
	}
	
	
}
