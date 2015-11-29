using UnityEngine;
using System.Collections;

public class ColorRollOver : MonoBehaviour {

	[Header("Light up when ball rolls over")]
	[Space(1)]
	public Color32 FromColor;
	public Color32 ToColor;
	
	private Color32 savedFromColor;
	private Color32 savedToColor;
	
	private float Duration=1f;
	public float rampUpDuration = 0.3f;
	public float rampDownDuration = 2f;
	private SpriteRenderer sprite; //sprite
	private float ratio;
	private	float deltaTime;
	private float savedTime;
	
	
	void Awake () 
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();		
		sprite.color = FromColor;
		savedFromColor = FromColor;
		savedToColor = ToColor;
		ToColor = FromColor; 
	}
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			savedTime = Time.time;
			FromColor = sprite.color;
			ToColor = savedToColor;	
			Duration = rampUpDuration;						
		}
	}

	public void OnTriggerExit2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			savedTime = Time.time;							
			FromColor = sprite.color;
			ToColor = savedFromColor;
			Duration = rampDownDuration;							
		}
	}
	
	
	//TODO no need to run this every frame?		
	void Update () 
	{
		{
			deltaTime = Time.time - savedTime;
			ratio = Mathf.Min(deltaTime/Duration,1f);
			sprite.color = Color32.Lerp(FromColor,ToColor,ratio);
		}
	}
}
