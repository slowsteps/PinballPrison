using UnityEngine;
using System.Collections;

public class ColorRollOver2 : MonoBehaviour {

	[Header("Light up when ball rolls over")]
	[Space(1)]
	private Color32 FromColor;
	public Color32 ToColor;
	
	private SpriteRenderer sprite;
	public float sprayTotal = 0f;
	private bool isSpraying = false;
	private Rigidbody2D ballRigidbody;
	private float fadeoutSpeed = 0.1f;
	
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
			sprayTotal = sprayTotal + ballRigidbody.velocity.magnitude;
		}
		
		sprayTotal = Mathf.Clamp01(sprayTotal - fadeoutSpeed);
		sprite.color = Color32.Lerp(FromColor,ToColor,sprayTotal);
	}
}
