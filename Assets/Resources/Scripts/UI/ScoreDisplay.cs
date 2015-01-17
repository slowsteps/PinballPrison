﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;
	public static ScoreDisplay instance;
	private Text ScoreText;

	void Awake()
	{
		instance = this;
		enabled = false;
		EventManager.Subscribe(OnEvent);
		ScoreText = GetComponent<Text>();
		//gameObject.SetActive(false);
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





}