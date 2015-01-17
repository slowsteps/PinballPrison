using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreIncreaseDisplay : MonoBehaviour {

	private Text ScoreIncreaseLabel;

	// Use this for initialization
	void Start () 
	{
		ScoreIncreaseLabel = gameObject.GetComponent<Text>();
		ScoreIncreaseLabel.text = "";
	}
	
	public void SetText(string inString)
	{
		ScoreIncreaseLabel.text = inString;
		StartCoroutine(ClearText());
	}

	IEnumerator ClearText()
	{
		yield return new WaitForSeconds(0.5f);
		ScoreIncreaseLabel.text = "";
	}

}
