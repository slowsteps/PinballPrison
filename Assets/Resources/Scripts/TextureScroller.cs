using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;

public class TextureScroller : MonoBehaviour {

	public float ScrollSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * ScrollSpeed;
		gameObject.GetComponent<SpriteRenderer>().renderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
