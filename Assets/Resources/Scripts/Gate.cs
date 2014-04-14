using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gate : TargetGroupEffect {

	//private List<Target> targets;
	public bool isBarrierActive = true;
	private bool isAllActivated = false;

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
			
		gameObject.SetActive(!isAllActivated);
		SwitchOn();
	}
		
			
	private void SwitchOn()
	{
		if (isBarrierActive && isAllActivated) gameObject.SetActive(false);
		if (isBarrierActive && !isAllActivated) gameObject.SetActive(true);
		if (!isBarrierActive && isAllActivated) gameObject.SetActive(true);
		if (!isBarrierActive && !isAllActivated) gameObject.SetActive(false);
	}
						
}
