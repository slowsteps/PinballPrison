using UnityEngine;
using System.Collections;


public class Scaler : MonoBehaviour {
	
	public float TimeOffset = 0f; // wait before kicking off the animation
	public float ScaleTime = 1f; // time in seconds from original scale to zero scale
	public float LoopDelay = 0f; // pause before loop rewind
	public Vector3 toScale;

	void Start()
	{
		Invoke("StartScaleAnimation",TimeOffset);
	}
	
	private void StartScaleAnimation()
	{
		//check http://itween.pixelplacement.com/documentation.php for help on iTween
		iTween.ScaleTo(gameObject,iTween.Hash("scale",toScale,"time",ScaleTime,"delay",LoopDelay,"looptype","loop","easetype",iTween.EaseType.linear));	
	}
	
}
