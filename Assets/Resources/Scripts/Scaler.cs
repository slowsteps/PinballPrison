using UnityEngine;
using System.Collections;


public class Scaler : MonoBehaviour {
	
	public float TimeOffset = 0f; // wait before kicking off the animation
	public float ScaleTime = 1f; // time in seconds from original scale to zero scale
	public float LoopDelay = 0f; // pause before loop rewind
	//public Vector3 toScale;
	public float MaxScale = 1f;
	public float MinScale = 0f;
	public bool IsInverted = false;
	private Vector3 OrigScale;

	void Start()
	{
		OrigScale = transform.localScale;
		transform.localScale = OrigScale * MaxScale;
		Invoke("StartScaleAnimation",TimeOffset);
	}
	
	private void StartScaleAnimation()
	{
		//check http://itween.pixelplacement.com/documentation.php for help on iTween
		if (IsInverted)
		{
			iTween.ScaleFrom(gameObject,iTween.Hash("scale",OrigScale*MinScale,"time",ScaleTime,"delay",LoopDelay,"looptype","loop","easetype",iTween.EaseType.linear));			
		}
		else
		{
			iTween.ScaleTo(gameObject,iTween.Hash("scale",OrigScale*MinScale,"time",ScaleTime,"delay",LoopDelay,"looptype","loop","easetype",iTween.EaseType.linear));	
		}
	}
	
}
