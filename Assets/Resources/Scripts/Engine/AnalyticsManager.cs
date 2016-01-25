using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;



public class AnalyticsManager : MonoBehaviour {
	
	

	
	public void Start() 
	{
		EventManager.Subscribe(OnEvent);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			Analytics.CustomEvent("levelStart", new Dictionary<string, object> {{"currentLevel",GameManager.instance.currentLevel}});
			break;
		case EventManager.EVENT_BALL_EXIT:
			Analytics.CustomEvent("ballExit", new Dictionary<string, object> {{"currentLevel",GameManager.instance.currentLevel},{"balls",GameManager.instance.balls}});
			break;
		case EventManager.EVENT_BALL_DEATH:
			Analytics.CustomEvent("LevelTryFail", new Dictionary<string, object> {{"currentLevel",GameManager.instance.currentLevel},{"reason","ballDeath"}});
			break;
		case EventManager.EVENT_OUT_OF_TIME:
			Analytics.CustomEvent("LevelTryFail", new Dictionary<string, object> {{"currentLevel",GameManager.instance.currentLevel},{"reason","outOfTime"}});
			break;		
		case EventManager.EVENT_TILT_START:
			Analytics.CustomEvent("tilt", new Dictionary<string, object> {{"currentLevel",GameManager.instance.currentLevel}});
			break;
			
		}
	}
	
	
	

	
}

