using UnityEngine;
using System.Collections;

public class TextFeedback : MonoBehaviour {

	public float lifeTime = 0;
	public Color toColor;
	private Color origColor;
	private float savedTime = 0;
	public float fadeSpeed = 0;
	private GameObject sourceGameObject;
	private Camera GUIBackgroundCamera;
	private Camera scrollCam;
	
	
	
	
	public static void Display(string inText,GameObject inSource=null)
	{

		GameObject go =  Instantiate(Resources.Load("Prefabs/Objects/TextFeedback_Prefab")) as GameObject;
		if (inSource) go.GetComponent<TextFeedback>().sourceGameObject = inSource;
		go.GetComponent<TextFeedback>().SetText(inText);
		
	}

	// Use this for initialization
	void Awake () {
		savedTime = Time.time;
		origColor = guiText.color;
		GUIBackgroundCamera = GameObject.Find("GUIBackgroundCamera").camera;
		scrollCam  = GameObject.Find("ScrollCam").camera;
	}
	
	// Update is called once per frame
	void Update () {
		fadeSpeed = fadeSpeed + Time.deltaTime;
		guiText.color = Color.Lerp(origColor,toColor,fadeSpeed);
		if ( (Time.time - savedTime) > lifeTime) Destroy(gameObject); 
		
	}

	public void SetText(string inText)
	{
		guiText.text = inText;
		
		if (sourceGameObject) transform.position =  new Vector3(0.5f,scrollCam.WorldToViewportPoint(sourceGameObject.transform.position).y,0f);
//		if (!gameObject.GetComponent<iTween>()) iTween.MoveTo(gameObject,iTween.Hash("x",-1f,"time",1.5f,"easeType",iTween.EaseType.easeInBack));
	}

}
