using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {
	
	
	public float bumpForce = 400f;
	public int bumpScore = 1;
	public float scaleEffectAmp = 0.2f;
	public float scaleEffectTime = 0.5f;
	public Sprite hitEffectSprite;
	public Color hitEffectFromColor;
	private Color hitEffectToColor;
	public float radius = 1.5f;
	private GameObject hitEffectGameObject;
	private SpriteRenderer hitEffectSpriteRenderer;
	private float curFade = 0f;
	
	
	public void Start()
	{
		if (hitEffectSprite)
		{
			hitEffectToColor = new Color(0,0,0,0);
			hitEffectGameObject = new GameObject("hitEffectGameObject-"+name);
			hitEffectGameObject.transform.position = gameObject.transform.position;
			hitEffectGameObject.transform.localScale = gameObject.transform.localScale * radius;
			hitEffectSpriteRenderer = hitEffectGameObject.AddComponent<SpriteRenderer>();
			hitEffectSpriteRenderer.sprite = hitEffectSprite;
			hitEffectSpriteRenderer.color = hitEffectFromColor;
			hitEffectSpriteRenderer.sortingOrder = -1;
			hitEffectSpriteRenderer.enabled = false;
			enabled = false;
			hitEffectGameObject.transform.parent = Level.instance.transform;
		}
	}
	
	
	public void OnCollisionEnter2D (Collision2D inColl)
	{
		inColl.rigidbody.AddForce(-bumpForce * inColl.contacts[0].normal);
		GameManager.instance.AddToScore(bumpScore);
		if (!gameObject.GetComponent<iTween>())
		{
			iTween.PunchScale(gameObject,iTween.Hash("amount",scaleEffectAmp * Vector3.one,"time",scaleEffectTime));
			if (hitEffectSprite) Play();
		}
	}
	
	public void Play()
	{	
		hitEffectSpriteRenderer.enabled = true;
		enabled = true;
		curFade = 0f;
	}
	
	public void Update()
	{
		if (hitEffectSprite)
		{
			curFade = curFade + Time.deltaTime;
			hitEffectSpriteRenderer.color = Color.Lerp(hitEffectFromColor,hitEffectToColor,curFade);
			if (curFade>1) 
			{
				curFade = 0f;
				hitEffectSpriteRenderer.enabled = false;
				enabled = false;
			}
		}
	}
	
	
}