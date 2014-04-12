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
			break;
		case EventManager.EVENT_BALL_DEATH:
			UpdateBalls(-1);
			break;
		case EventManager.EVENT_BALL_EXIT:
			InitBalls();
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			UpdateLives(-1);
			InitBalls();
			break;
		case EventManager.EVENT_MENU_SHOW:
			StartLivesRefill();
			break;	
		}
	}
	
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			Instantiate(Resources.Load("Prefabs/Ball_Prefab"));
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
	
		
				
	private void InitBalls()
	{
		balls = startBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		score = 0;
	}
	
	private void InitLives()
	{
		lives = 2;
		EventManager.fireEvent(EventManager.EVENT_LIVES_UPDATED);
	}
	
	public void AddToScore(int extraScore)
	{
		//check if updated score breaks thru threshold
		if ( !isMinimimScoreReached && (score + extraScore) > Level.instance.minimumScore )
		{
			EventManager.fireEvent(EventManager.EVENT_MINIMUMSCORE_REACHED);
			isMinimimScoreReached = true;
		}
		score = score + extraScore;
		
	}
					
}
