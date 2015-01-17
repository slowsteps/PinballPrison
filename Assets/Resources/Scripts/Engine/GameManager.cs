using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	public int startBalls = 3;
	public int balls;
	public long score = 0;
	public int ScoreMultiplier = 1;
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
		print("Start GameManager");
		EventManager.fireEvent(EventManager.EVENT_GAME_START);
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
		}
	}
	
		
	private void BallShot()
	{
		if (Level.instance.hasMaxShots && shotsPlayed == Level.instance.allowedShots)
		{
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_SHOTS);
		}
	}
	
	
	
					
																
	private void UpdateBalls(int deltaBalls = 0)
	{
		balls = balls + deltaBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		if (balls == 0) EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
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
			break;
		case levelOverReasons.OUT_OF_SHOTS:
			UIManager.instance.SetMessage("Game over, out of shots");
			break;
		case levelOverReasons.OUT_OF_TIME:
			UIManager.instance.SetMessage("Game over, out of time");
			break;
		case levelOverReasons.QUIT:
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
