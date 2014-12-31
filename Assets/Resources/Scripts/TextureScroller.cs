using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class TextureScroller : MonoBehaviour {

	public float speed;
	private float savedTime = 0;
	private float scrollDistance = 0;
	private Material DetectedMaterial = null;
	private Material ScrollMaterial = null;



	// Use this for initialization
	void Start () {


		ScrollMaterial = Resources.Load("Materials/Scroll_Material", typeof(Material)) as Material;
		
		if (gameObject.GetComponent<Image>()) 
		{
			gameObject.GetComponent<Image>().material = ScrollMaterial;
			DetectedMaterial = gameObject.GetComponent<Image>().material;
		}
		else if (gameObject.GetComponent<SpriteRenderer>()) 
		{
			gameObject.GetComponent<SpriteRenderer>().material = ScrollMaterial;
			DetectedMaterial = gameObject.GetComponent<SpriteRenderer>().material;
		}

//		if (gameObject.GetComponent<Image>()) DetectedMaterial = gameObject.GetComponent<Image>().material;
//		else if (gameObject.GetComponent<SpriteRenderer>()) DetectedMaterial = gameObject.GetComponent<SpriteRenderer>().material;
						

		savedTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		scrollDistance = speed*(Time.time - savedTime);
		if (Mathf.Abs(scrollDistance) > 1) savedTime = Time.time;
		//gameObject.renderer.material.mainTextureOffset = new Vector2(scrollDistance,0);
		DetectedMaterial.SetTextureOffset("_MainTex", new Vector2(scrollDistance, 0));
		
	}
	
	
	
	
}
