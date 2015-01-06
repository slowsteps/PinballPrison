using UnityEngine;
using System.Collections;

public class ScoreMultiplier : TargetGroupEffect {

	private bool isAllActivated = false;
	public int Multiplier = 1;
	public float resetTime = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	public override void AddTarget(Target inTarget)
	{
		targets.Add(inTarget);
	}
	
	public override void ReportTargetHit(Target inTarget)
	{
		isAllActivated = true;
		foreach (Target aTarget in targets)
		{
			if (!aTarget.isActivated) 
			{
				isAllActivated=false;
				break;
			}
		}
		
		Switch();
		
	}
	
	
	private void Switch()
	{
		if (isAllActivated)
		{
			GameManager.instance.ScoreMultiplier = Multiplier;
			EventManager.fireEvent(EventManager.EVENT_SCORE_MULTIPLIER);
		}
	}
}
