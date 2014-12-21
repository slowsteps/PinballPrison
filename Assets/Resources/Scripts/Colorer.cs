using UnityEngine;
using System.Collections;

public class Colorer : MonoBehaviour {

	//public Vector3 spinAngles;
	public Vector4 firstColor;
	public Vector4 secondColor;
	//public float delay=0;
	private Color newColour;

	
	void Start() {
		//Spriterenderer renderer = gameObject.GetComponent(SpriteRenderer);
		//Color(firstColor);
		//GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
		GetComponent<SpriteRenderer>().color = firstColor;
		//newColour = firstColor;
	}

	void Update () {
		//GetComponent<SpriteRenderer>().color = firstColor;
		//renderer.color = new Color(0f, 0f, 0f, 1f);
		//transform.Rotate(1f*spinAngles*(Time.deltaTime));

		//GetComponent<SpriteRenderer>().color = vector4.lerp(firstColor, secondColor, 0.1f);
	}


}
