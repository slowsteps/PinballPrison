using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;

public class ColorCycle : MonoBehaviour {
	
	public Color32 FromColor;
	public Color32 ToColor;
	public float Duration=1f;
	public float Delay=0f;
	public Patterns Pattern = Patterns.Sin;
	private SpriteRenderer sprite;
	private float ratio;
	private	float MyTime;
	 
	public enum Patterns {Sin,Triangle,Saw,Block};


	void Start () 
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();		
	}
	
	
	void Update () 
	{
		MyTime = Time.time + Delay;
		if (Pattern == Patterns.Sin) ratio = 0.5f + (0.5f*Mathf.Sin(MyTime/Duration));
		if (Pattern == Patterns.Saw) ratio = Mathf.Repeat(MyTime,Duration);
		if (Pattern == Patterns.Triangle) ratio = Mathf.PingPong(MyTime,Duration);
		if (Pattern == Patterns.Block) ratio =  Mathf.Round(Mathf.Repeat(MyTime/Duration,1f));
		
		sprite.color = Color32.Lerp(FromColor,ToColor,ratio);
	}
	
	
}
