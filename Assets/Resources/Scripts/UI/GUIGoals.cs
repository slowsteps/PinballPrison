using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GUIGoals : MonoBehaviour {

	public Text ScoreLabel;
	public Text ShotsLabel;
	public Text Timelabel;
	public Text KeysLabel;
	
	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		Clear();
		gameObject.SetActive(false);
	}
	
	private void Clear()
	{
		ScoreLabel.text = "";
		ShotsLabel.text = "";
		Timelabel.text = "";
		KeysLabel.text = "";
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.LEVEL_BUTTON_CLICKED:
			gameObject.SetActive(true);
			break;
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(true);
			Clear();
			if (Level.instance.hasMinScore) ScoreLabel.text = "Required score: " + Level.instance.requiredScore;
			if (Level.instance.hasMaxShots) ShotsLabel.text = "Allowed shots: " + Level.instance.allowedShots;
			if (Level.instance.hasCollectables) KeysLabel.text = "Required keys: " + Level.instance.requiredCollectables;
			if (Level.instance.hasMaxTime) 
			{	
				TimeSpan ts = TimeSpan.FromSeconds(Level.instance.allowedTime);
				Timelabel.text = "Allowed time: " + ts.Minutes + ":" + ts.Seconds;
			}
			break;
		}
	}
	
	public void OnOkButton()
	{
		EventManager.fireEvent(EventManager.GOALS_OK_BUTTON_CLICKED);
		gameObject.GetComponent<Animator>().Play("LevelGoalsHide",-1,0f);
		SoundManager.instance.PlaySound("LevelStart_SFX");
	}
	
	public void OnAppearComplete()
	{
		print ("LevelGoals anim complete");
	}
	
	public void OnHideAnimComplete()
	{
		gameObject.GetComponent<Animator>().playbackTime = 0f;;
		gameObject.SetActive(false);
		print ("LevelGoals HideAnim complete");
	}
}
