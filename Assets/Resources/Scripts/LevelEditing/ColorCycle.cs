using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class ColorCycle : MonoBehaviour {
	
	[Header("Works for sprites and UI Images")]
	[Space(1)]
	public Color32 FromColor;
	public Color32 ToColor;
	public float Duration=1f;
	public float Delay=0f;
	public Patterns Pattern = Patterns.Triangle;
	private SpriteRenderer sprite; //sprite
	private Image img; //ui image
	public float ratio;
	private	float MyTime;
	 
	public enum Patterns {Sin,Triangle,Saw,Block};


	void Start () 
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();		
		if (!sprite) img = gameObject.GetComponent<Image>();
	}
	
	
	void Update () 
	{
		MyTime = Time.time + Delay;
		if (Pattern == Patterns.Sin) ratio = 0.5f + (0.5f*Mathf.Sin(MyTime/Duration));
		if (Pattern == Patterns.Saw) ratio = Mathf.Repeat(MyTime/Duration,1f);
		if (Pattern == Patterns.Triangle) ratio = Mathf.PingPong(MyTime/Duration,1f);
		if (Pattern == Patterns.Block) ratio =  Mathf.Round(Mathf.Repeat(MyTime/Duration,1f));
		
		if (sprite) sprite.color = Color32.Lerp(FromColor,ToColor,ratio);
		else if (img) img.color = Color32.Lerp(FromColor,ToColor,ratio);
	}
	
	
}
