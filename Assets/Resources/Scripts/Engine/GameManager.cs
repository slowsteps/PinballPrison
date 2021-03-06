﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager instance;
	
	public int startBalls = 3;
	public float tiltCooldownTime = 3f;
	public int balls;
	public long score = 0;
	public int ScoreMultiplier = 1;
	public int currentLevel = 1; // player progression
	public int loadedLevel = 0; // level loaded in scene
	public int totalLevels = 40;
	public bool hasPlayerClicked = false;
	private bool isMinimimScoreReached = false;
	private enum levelOverReasons {OUT_OF_BALLS,OUT_OF_SHOTS,OUT_OF_TIME,EXIT_REACHED,COLLECTABLES_FOUND,QUIT};
	private levelOverReasons levelOverReason;
	private bool isScoreAdditionAllowed = true;
	public ParticleSystem scoreParticles; // display point increase
	public ParticleSystem touchParticles; // accentuate impact
	public Texture[] scoreTextures;
	
	

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		instance = this;
		
		
		
		
		if (PlayerPrefs.HasKey("isFirstPlay"))
		{
			//picking up from previous play
			print ("found old game");
			if (PlayerPrefs.HasKey("balls")) balls = PlayerPrefs.GetInt("balls");
			if (PlayerPrefs.HasKey("currentLevel")) currentLevel = PlayerPrefs.GetInt("currentLevel");
		}
		else
		{
			//clean start .
			balls = startBalls;
			PlayerPrefs.SetInt("balls",balls);
			PlayerPrefs.SetInt("isFirstPlay",0);
			currentLevel = 1;
		}
		
		
		
		
		InitBalls();
		LoadGameLevel(currentLevel);
		print ("firing game start");
		EventManager.fireEvent(EventManager.EVENT_GAME_START);
		
	}

	

	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			isMinimimScoreReached = false;
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
		case EventManager.EVENT_OUT_OF_BALLS:
			OnGameOver(levelOverReasons.OUT_OF_BALLS);
			break;
		case EventManager.EVENT_OUT_OF_TIME:
			OnGameOver(levelOverReasons.OUT_OF_TIME);
			break;		
		case EventManager.EVENT_TILT_START:
			StartTilt();
			break;
		
		}
	}
	
			
	private void StartTilt()
	{
		isScoreAdditionAllowed = false;
		Invoke("EndTilt",tiltCooldownTime);
	}
	
	private void EndTilt()
	{
		isScoreAdditionAllowed = true;
		EventManager.fireEvent(EventManager.EVENT_TILT_END);
	}
	
					
																
	private void UpdateBalls(int deltaBalls = 0)
	{
		balls = balls + deltaBalls;
		if (balls < 0) balls = 0;
		PlayerPrefs.SetInt("balls",balls);
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		if (balls == 0) EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
	}
	
	private void OnGameOver(levelOverReasons reason)
	{
		//TODO can this case statement be replaced by events directly?
		switch (reason)
		{
		case levelOverReasons.EXIT_REACHED:
			GUIEndOfLevel.instance.SetMessage(Level.instance.SuccesMessage);
			currentLevel++;
			PlayerPrefs.SetInt("currentLevel",currentLevel);
			EventManager.fireEvent(EventManager.EVENT_LEVEL_INCREASE);
			break;
		case levelOverReasons.OUT_OF_BALLS:
			//TODO there is an out of balls event, GUIEndOFLevel should respond to that instead of hard call from here
			EventManager.fireEvent(EventManager.EVENT_LEVEL_FAILED);
			GUIEndOfLevel.instance.SetMessage("Out of balls, " + Level.instance.FailMessage);
			break;
		case levelOverReasons.OUT_OF_TIME:
			UpdateBalls(-1);
			EventManager.fireEvent(EventManager.EVENT_LEVEL_FAILED);
			GUIEndOfLevel.instance.SetMessage("Out of time, " + Level.instance.FailMessage);
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
		//balls = startBalls;
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		score = 0;
	}
	
	
	public void AddToScore(int extraScore,GameObject sender)
	{
		if (isScoreAdditionAllowed)
		{
		
			//check if updated score breaks thru threshold
			if ( !isMinimimScoreReached && (score + extraScore) >= Level.instance.requiredScore )
			{
				if (Level.instance.hasMinScore) EventManager.fireEvent(EventManager.EVENT_MINIMUMSCORE_REACHED);
				isMinimimScoreReached = true;
			}
			score = score + (ScoreMultiplier * extraScore);

			int index = 0;
			if (extraScore == 100) index = 0;
			if (extraScore == 250) index = 1;
			if (extraScore == 500) index = 2;
			if (extraScore == 1000) index = 3;
			
			scoreParticles.gameObject.transform.position = sender.transform.position;
			//TODO cache reference - do we still use this??
			scoreParticles.GetComponent<Renderer>().material.mainTexture = scoreTextures[index];
			scoreParticles.Emit(1);
			
			touchParticles.gameObject.transform.position = sender.transform.position;
			touchParticles.Emit(3);
			
			
		}
	}
				
					
	public void OpenAllLevels()
	{
	
		if (currentLevel == 99) currentLevel = 1;
		else currentLevel = 99;
		EventManager.fireEvent(EventManager.EVENT_LEVEL_INCREASE);
	}	
	
	
	public void LoadGameLevel(int levelNumber) 
	{

		hasPlayerClicked = true;
		//Hack for level loading from menu - calling this directly
		currentLevel = levelNumber;
		//end Hack
		
		if (Application.CanStreamedLevelBeLoaded("Level"+levelNumber)) 
		{
			Application.LoadLevelAdditive("Level"+levelNumber);
			SoundManager.instance.PlaySound("Select_SFX");
			loadedLevel = levelNumber;
			
		}
		else print("can't load scene Level"+levelNumber) ;	
		
		
		
																						
	}	
	
	
	
	//TODO collapse try again and next level buttons actions into one continue button
	public void ContinuePlay()
	{
		//TODO check for reaching last level
		LoadGameLevel(currentLevel);
	}
	
	public void LoadNextGameLevel() 
	{
		if (NextLevelIsAvailable()) LoadGameLevel(loadedLevel+1);
		else print ("last level reached");
	}
	
	public bool NextLevelIsAvailable()
	{
		if (loadedLevel < totalLevels) return true;
		else return false;
	}
				
	public void AddBall()
	{
		balls++;
		EventManager.fireEvent(EventManager.EVENT_EXTRA_BALL);
		EventManager.fireEvent(EventManager.EVENT_BALLS_UPDATED);
		
	}	
	
	public void ResetSaveGame()
	{
		PlayerPrefs.DeleteAll();
		currentLevel = 1;
		InitBalls();
		ContinuePlay();
		EventManager.fireEvent(EventManager.EVENT_GAME_RESET);
	}																								
																																																	

																																									
}
