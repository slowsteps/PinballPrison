using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;
	public static ScoreDisplay instance;
	private Text ScoreText;

	void Start()
	{
		instance = this;
		enabled = false;
		EventManager.Subscribe(OnEvent);
		ScoreText = GetComponent<Text>();
		//gameObject.SetActive(false);
		StartCoroutine(AnimateScore());
	}


	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			enabled = true;
			UpdateScoreDisplay();
			break;
		}
	}
	
	
	public void UpdateScoreDisplay() 
	{
		//ScoreText.text = string.Format("{0:#,###0}",GameManager.instance.score);
		ScoreText.text = string.Format("{0:#,###0}",GameManager.instance.score);
		ScoreText.text = GameManager.instance.score.ToString("000,000,000,000");
	}

	IEnumerator AnimateScore()
	{
		//print ("Kicking off IENumerator " + Time.time);
		for (;;)
		{
			displayScore = Mathf.CeilToInt( Mathf.Lerp(displayScore,GameManager.instance.score,0.5f));
			ScoreText.text = string.Format("{0:#,###0}",displayScore);
			ScoreText.text = displayScore.ToString("000,000,000,000");
			yield return new WaitForSeconds(0.1f);
		}
	}



}
