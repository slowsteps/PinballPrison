using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private int startBalls = 3;
	public int balls;
	public int score = 0;


	void Start () {
		EventManager.Subscribe(OnEvent);
		instance = this;
		InitLevels();
		InitBalls();
	}


	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_BALL_DEATH:
			UpdateBalls(-1);
			break;
		case EventManager.EVENT_BALL_EXIT:
			InitBalls();
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
	
	private void UpdateBalls(int deltaBalls = 0)
	{
		balls = balls + deltaBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		if (balls == 0) EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
	}
	
	private void InitBalls()
	{
		balls = startBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		score = 0;
	}
	
	private void InitLevels()
	{
//		Instantiate(Resources.Load("Prefabs/Level1_Prefab"));
//		EventManager.fireEvent(EventManager.EVENT_LEVEL_START);
	}
	
	public void AddToScore(int extraScore)
	{
		//check if updated score breaks thru threshold
		if ( score < Level.instance.minimumScore && (score + extraScore) > Level.instance.minimumScore ) EventManager.fireEvent(EventManager.EVENT_MINIMUMSCORE_REACHED);
		score = score + extraScore;
		
	}
					
}
