using UnityEngine;
using System.Collections;

public class ComboEffect : TargetGroupEffect {


	private bool isAllActivated = false;
	public int scoreValue = 0;


	void Start()
	{
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

		SwitchOn();
	}	
	
	
	private void SwitchOn()
	{
		if (isAllActivated) 
		{
			GameManager.instance.AddToScore(scoreValue,gameObject);
			if (GetComponent<ParticleSystem>()) 
			{
				GetComponent<ParticleSystem>().time = 0f;
				GetComponent<ParticleSystem>().Play();
			}
			gameObject.SetActive(true);
		}
	}
	
}
