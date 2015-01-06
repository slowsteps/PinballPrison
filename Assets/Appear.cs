using UnityEngine;
using System.Collections;

public class Appear : TargetGroupEffect {

	private bool isAllActivated = false;
	public int Multiplier = 1;
	public float ResetTime = 10f;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
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
	
	private void Hide()
	{
		gameObject.SetActive(false);
	}
	
	void OnDestroy()
	{
		CancelInvoke("ResetMultiplier");
	}
	
	private void Switch()
	{
		if (isAllActivated)
		{
			gameObject.SetActive(true);
			Invoke("Hide",ResetTime);
		}
	}
}
