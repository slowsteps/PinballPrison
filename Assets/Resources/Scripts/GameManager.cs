using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public int lives = 5;
	public int startBalls = 3;
	public int balls;
	public int collectables = 0;
	public int score = 0;
	public int ScoreMultiplier = 1;
	public int livesRefillTime = 10;
	public ScoreIncreaseDisplay ScoreUpdateLabel;
	
	private bool isMinimimScoreReached = false;
	public int shotsPlayed = 0;
	private enum levelOverReasons {OUT_OF_BALLS,OUT_OF_SHOTS,OUT_OF_TIME,EXIT_REACHED,COLLECTABLES_FOUND,QUIT};
	private levelOverReasons levelOverReason;
	

	void Start () 
	{
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
		case EventManager.EVENT_QUIT:
			OnGameOver(levelOverReasons.QUIT);
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
		case EventManager.EVENT_COLLECTABLE_FOUND:
			IncreaseCollectables();
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
	
	private void IncreaseCollectables()
	{
		collectables++;
		if (collectables == Level.instance.requiredCollectables) 
		{
			EventManager.fireEvent(EventManager.EVENT_ALL_COLLECTABLES_FOUND);
			//OnGameOver(levelOverReasons.COLLECTABLES_FOUND);
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
			UIManager.instance.SetMessage("Awesomeness! \n Level complete");
			break;
		case levelOverReasons.OUT_OF_BALLS:
			UIManager.instance.SetMessage("Game over, out of balls");
			UpdateLives(-1);		
			break;
		case levelOverReasons.OUT_OF_SHOTS:
			UIManager.instance.SetMessage("Game over, out of shots");
			UpdateLives(-1);
			break;
		case levelOverReasons.OUT_OF_TIME:
			UIManager.instance.SetMessage("Game over, out of time");
			UpdateLives(-1);
			break;
		case levelOverReasons.QUIT:
			UpdateLives(-1);
			break;
		case levelOverReasons.COLLECTABLES_FOUND:
			UIManager.instance.SetMessage("All collectables found!");
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
		score = score + (ScoreMultiplier * extraScore);
		if (ScoreDisplay.instance) ScoreDisplay.instance.UpdateScoreDisplay();
		if (ScoreUpdateLabel) ScoreUpdateLabel.SetText("+" +  ScoreMultiplier * extraScore);
	}
					
}
