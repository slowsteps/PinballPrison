using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;

public class BallEffects : MonoBehaviour {


	public Color32 SlowColor = Color.gray;
	public Color32 FastColor = Color.green;
	public float Damping = 0.1f;
	private Rigidbody2D BallRigidBody;
	private SpriteRenderer BallSpriteRenderer; 
	private float XfadeAmount;
	

	
	void Start () 
	{
		BallRigidBody = gameObject.rigidbody2D;
		BallSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		BallSpriteRenderer.color = SlowColor;
	}
	
	
	void Update () 
	{
		XfadeAmount = Damping * BallRigidBody.velocity.magnitude;
		BallSpriteRenderer.color = Color32.Lerp(SlowColor,FastColor,XfadeAmount);
	}
}
