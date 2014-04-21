using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gate : TargetGroupEffect {

	public bool isBarrierActive = true;
	private bool isAllActivated = false;
	public bool hasResetTime = false;
	public float resetTime = 10f;

	void Awake()
	{
		gameObject.SetActive(isBarrierActive);
		targets = new List<Target>();
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
		if (gameObject.activeSelf && isAllActivated) 
		{
			if (hasResetTime) Invoke("ResetGate",resetTime);
			gameObject.SetActive(false);
			TextFeedback.Display("Gate Open",gameObject);
		}

		else if (!gameObject.activeSelf && !isAllActivated) 
		{
			gameObject.SetActive(true);
			TextFeedback.Display("Gate Closed",gameObject);
		}
						
		else if (!gameObject.activeSelf && isAllActivated) 
		{
			if (hasResetTime) Invoke("ResetGate",resetTime);
			gameObject.SetActive(true);
			iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
		}
		
	}

	private void ResetGate()
	{
		if (gameObject.activeSelf) gameObject.SetActive(false);
		else {
			gameObject.SetActive(true);
			iTween.PunchScale(gameObject,new Vector3(0.3f,0.3f,0.3f),2f);
		}
		isAllActivated = false;
		foreach (Target aTarget in targets) aTarget.Reset();
	}					
																		
}
