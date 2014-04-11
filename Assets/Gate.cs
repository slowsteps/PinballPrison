using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gate : MonoBehaviour {

	private List<Target> targets;
	public bool isClosed = true;
	private bool isAllActivated = false;

	void Start()
	{
		gameObject.SetActive(isClosed);
		targets = new List<Target>();
	}

	public void AddToTargets(Target inTarget)
	{
		targets.Add(inTarget);
	}

	public void ReportTargetHit(Target inTarget)
	{
		isAllActivated = true;
		foreach (Target aTarget in targets)
		{
			print (aTarget.name + " - " + aTarget.isActivated);
			if (!aTarget.isActivated) 
			{
				isAllActivated=false;
				break;
			}
		}
		//if (isAllActivated) isClosed = !isClosed;
		
		gameObject.SetActive(!isAllActivated);
	}
			
}
