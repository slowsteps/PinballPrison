using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private int startBalls = 3;
	public int balls;
	public int score = 0;
	public int lives = 5;
	public int livesRefillTime = 10;
	private bool isMinimimScoreReached = false;
	public int shotsPlayed = 0;
	private enum levelOverReasons {OUT_OF_BALLS,OUT_OF_SHOTS,OUT_OF_TIME,EXIT_REACHED};
	private levelOverReasons levelOverReason;

	void Start () {
		EventManager.Subscribe(OnEvent);
		instance = this;
		InitBalls();
		InitLives();
		EventManager.fireEvent(EventManager.EVENT_GAME_START);
	}

	

	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			isMinimimScoreReached = false;
			StopLivesRefill();
			shotsPlayed = 0;
			break;
		case EventManager.EVENT_BALL_DEATH:
			UpdateBalls(-1);
			break;
		case EventManager.EVENT_BALL_EXIT:
			OnGameOver(levelOverReasons.EXIT_REACHED);
			break;
		case EventManager.EVENT_BALL_SHOT:
			BallShot();
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
		case EventManager.EVENT_MENU_SHOW:
			StartLivesRefill();
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
	
	private void StartLivesRefill()
	{
		InvokeRepeating("IncreaseLives",livesRefillTime,livesRefillTime);
	}

	private void StopLivesRefill()
	{
		CancelInvoke("IncreaseLives");
	}
	
	private void IncreaseLives()
	{
		if (lives<5) 
		{
			lives++;
			EventManager.fireEvent(EventManager.EVENT_LIVES_UPDATED);
			print ("lives incr " + lives);
		}
	}				
								
	private void UpdateBalls(int deltaBalls = 0)
	{
		balls = balls + deltaBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		if (balls == 0) EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
	}

	private void UpdateLives(int deltaLives = 0)
	{
		lives = lives + deltaLives;
		EventManager.fireEvent(EventManager.EVENT_LIVES_UPDATED);
		if (lives == 0) EventManager.fireEvent(EventManager.EVENT_OUT_OF_LIVES);
	}
	
	private void OnGameOver(levelOverReasons reason)
	{
		switch (reason)
		{
		case levelOverReasons.EXIT_REACHED:
			TextFeedback.Display("Level completed");
			GUIMessage.instance.SetText("Awesomeness! \n Level complete");			
			break;
		case levelOverReasons.OUT_OF_BALLS:
			TextFeedback.Display("Out of balls");
			GUIMessage.instance.SetText("Game over, out of balls");	
			UpdateLives(-1);		
			break;
		case levelOverReasons.OUT_OF_SHOTS:
			TextFeedback.Display("Out of  shots");			
			GUIMessage.instance.SetText("Game over, out of shots");
			UpdateLives(-1);
			break;
		case levelOverReasons.OUT_OF_TIME:
			TextFeedback.Display("Out of time");			
			GUIMessage.instance.SetText("Game over, out of time");
			UpdateLives(-1);
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
	
	private void InitLives()
	{
		EventManager.fireEvent(EventManager.EVENT_LIVES_UPDATED);
	}
	
	public void AddToScore(int extraScore)
	{
		//check if updated score breaks thru threshold
		if ( !isMinimimScoreReached && (score + extraScore) > Level.instance.requiredScore )
		{
			EventManager.fireEvent(EventManager.EVENT_MINIMUMSCORE_REACHED);
			isMinimimScoreReached = true;
		}
		score = score + extraScore;
		if (ScoreDisplay.instance) ScoreDisplay.instance.UpdateScoreDisplay();
	}
					
}
