using UnityEngine;
using System.Collections;

public class TextFeedback : MonoBehaviour {

	public float fadeOutTime = 0;
	private float savedTime = 0;
	private float fadeSpeed = 0;
	
	
	public static void Display(string inText)
	{
		GameObject go =  Instantiate(Resources.Load("Prefabs/Objects/TextFeedback_Prefab")) as GameObject;
		go.GetComponent<TextFeedback>().SetText(inText);
	}

	// Use this for initialization
	void Start () {
		savedTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		fadeSpeed = fadeSpeed + 0.01f;
		gameObject.transform.Translate(new Vector3(0,fadeSpeed*fadeSpeed*Time.deltaTime,0));
		if ( (Time.time - savedTime) > fadeOutTime) Destroy(gameObject); 
	}

	public void SetText(string inText)
	{
		guiText.text = inText;
	}

}
