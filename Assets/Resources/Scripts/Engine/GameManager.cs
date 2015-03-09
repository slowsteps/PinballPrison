using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	public int startBalls = 3;
	public float tiltCooldownTime = 3f;
	public int balls;
	public long score = 0;
	public int ScoreMultiplier = 1;
	public ScoreIncreaseDisplay ScoreUpdateLabel;
	public int currentLevel = 1; // player progression
	public int loadedLevel = 0;
	private bool isMinimimScoreReached = false;
	public int shotsPlayed = 0;
	private enum levelOverReasons {OUT_OF_BALLS,OUT_OF_SHOTS,OUT_OF_TIME,EXIT_REACHED,COLLECTABLES_FOUND,QUIT};
	private levelOverReasons levelOverReason;
	

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		instance = this;
		InitBalls();
	}

	

	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			isMinimimScoreReached = false;
			shotsPlayed = 0;
			break;
		case EventManager.EVENT_BALL_DEATH:
			UpdateBalls(-1);
			break;
		case EventManager.EVENT_BALL_EXIT:
			OnGameOver(levelOverReasons.EXIT_REACHED);
			break;
		case EventManager.EVENT_QUIT:
			OnGameOver(levelOverReasons.QUIT);
			break;
		case EventManager.EVENT_BALL_SHOT:
			//BallShot();
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			OnGameOver(levelOverReasons.OUT_OF_BALLS);
			break;
		case EventManager.EVENT_OUT_OF_SHOTS:
			OnGameOver(levelOverReasons.OUT_OF_SHOTS);
			break;
		case EventManager.EVENT_OUT_OF_TIME:
			OnGameOver(levelOverReasons.OUT_OF_TIME);
			break;		
		case EventManager.EVENT_TILT_START:
			StartTilt();
			break;
		
		}
	}
	
		
	private void BallShot()
	{
		if (Level.instance.hasMaxShots && shotsPlayed == Level.instance.allowedShots)
		{
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_SHOTS);
		}
	}
	
	private void StartTilt()
	{
		Invoke("EndTilt",tiltCooldownTime);
	}
	
	private void EndTilt()
	{
		EventManager.fireEvent(EventManager.EVENT_TILT_END);
	}
	
					
																
	private void UpdateBalls(int deltaBalls = 0)
	{
		balls = balls + deltaBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		if (balls == 0) EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
		
		if (Level.instance.hasMaxShots && shotsPlayed == Level.instance.allowedShots)
		{
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_SHOTS);
		}
		
		
	}
	
	private void OnGameOver(levelOverReasons reason)
	{
		switch (reason)
		{
		case levelOverReasons.EXIT_REACHED:
			GUIEndOfLevel.instance.SetMessage(Level.instance.SuccesMessage);
			currentLevel++;
			EventManager.fireEvent(EventManager.EVENT_LEVEL_INCREASE);
			break;
		case levelOverReasons.OUT_OF_BALLS:
			EventManager.fireEvent(EventManager.EVENT_LEVEL_FAILED);
			GUIEndOfLevel.instance.SetMessage("Out of balls\n " + Level.instance.FailMessage);
			break;
		case levelOverReasons.OUT_OF_SHOTS:
			EventManager.fireEvent(EventManager.EVENT_LEVEL_FAILED);
			GUIEndOfLevel.instance.SetMessage("Out of shots\n " + Level.instance.FailMessage);
			break;
		case levelOverReasons.OUT_OF_TIME:
			EventManager.fireEvent(EventManager.EVENT_LEVEL_FAILED);
			GUIEndOfLevel.instance.SetMessage("Out of time\n " + Level.instance.FailMessage);
			break;
		case levelOverReasons.QUIT:
			break;
		case levelOverReasons.COLLECTABLES_FOUND:
			GUIEndOfLevel.instance.SetMessage("All collectables found! ");
			break;
		}
		
		
		InitBalls();
		
	}
							
	private void InitBalls()
	{
		balls = startBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		score = 0;
	}
	
	
	public void AddToScore(int extraScore)
	{
		//check if updated score breaks thru threshold
		if ( !isMinimimScoreReached && (score + extraScore) >= Level.instance.requiredScore )
		{
			if (Level.instance.hasMinScore) EventManager.fireEvent(EventManager.EVENT_MINIMUMSCORE_REACHED);
			isMinimimScoreReached = true;
		}
		score = score + (ScoreMultiplier * extraScore);
		if (ScoreDisplay.instance) ScoreDisplay.instance.UpdateScoreDisplay();
		if (ScoreUpdateLabel) ScoreUpdateLabel.SetText("+" +  ScoreMultiplier * extraScore);
	}
				
					
	public void OpenAllLevels()
	{
	
		if (currentLevel == 99) currentLevel = 1;
		else currentLevel = 99;
		EventManager.fireEvent(EventManager.EVENT_LEVEL_INCREASE);
	}	
	
	
	public void LoadGameLevel(int levelNumber) 
	{
		Settings.hasPlayerClicked = true;
		if (Application.CanStreamedLevelBeLoaded("Level"+levelNumber)) 
		{
			Application.LoadLevelAdditive("Level"+levelNumber);
			SoundManager.instance.PlaySound("Select_SFX");
			loadedLevel = levelNumber;
		}
		else print("can't load scene Level"+levelNumber) ;						
	}															
}
