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
		//this is a barrier you want to dissappear
		if (isBarrierActive)
		{
			if (gameObject.activeSelf && isAllActivated) 
			{
				gameObject.SetActive(false);
				if (hasResetTime) Invoke("ResetGate",resetTime);
			}
		}
		//this is a barrier you want to appear
		else
		{						
			if (!gameObject.activeSelf && isAllActivated) 
			{
				gameObject.SetActive(true);
				if (hasResetTime) Invoke("ResetGate",resetTime);
			}
		}
		
	}

	private void ResetGate()
	{
		print ("resetgate " + name + "isBarrierActive="+ isBarrierActive);
		
		if (isBarrierActive) gameObject.SetActive(true);
		else gameObject.SetActive(false);
		isAllActivated = false;
		foreach (Target aTarget in targets) aTarget.Reset();
	}					
																		
}
