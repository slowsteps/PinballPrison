using UnityEngine;
using System.Collections;

public class ColorRollOver2 : MonoBehaviour {

	[Header("Light up when ball rolls over")]
	[Space(1)]
	private Color32 FromColor;
	public Color32 ToColor;
	
	private SpriteRenderer sprite;
	private float sprayTotal = 0f;
	private bool isSpraying = false;
	private static Rigidbody2D ballRigidbody;
	[Header("color ramp up speed")]
	[Range(0,1)]
	public float speedMultiplier = 0.2f;
	[Header("time (secs) to fade back to original color")]
	public float fadeoutDuration = 1f;
	
	
	void Start () 
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();		
		FromColor = sprite.color;
	}
	
	public void OnTriggerEnter2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			isSpraying = true;
		}
	}

	public void OnTriggerExit2D (Collider2D ball)
	{
		if (ball.tag == "ball") 
		{
			isSpraying = false;
		}
	}
	
	
	//TODO no need to run this every frame?		
	void Update () 
	{
	
		if (!ballRigidbody) ballRigidbody = Ball.instance.gameObject.GetComponent<Rigidbody2D>();
		
		if (isSpraying)
		{
			sprayTotal = sprayTotal + (speedMultiplier * ballRigidbody.velocity.magnitude);
		}
		
		sprayTotal = Mathf.Clamp01(sprayTotal - (Time.deltaTime / fadeoutDuration));
		sprite.color = Color32.Lerp(FromColor,ToColor,sprayTotal);
	}
}
