using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class EventManager {
	
	private List<Callbackmethod> callbackMethods; 

	public const string EVENT_GAME_START = "EVENT_GAME_START";
	public const string EVENT_GAME_COMPLETED = "EVENT_GAME_COMPLETED";
	public const string EVENT_GAME_RESET = "EVENT_GAME_RESET";
	public const string EVENT_LEVEL_START = "EVENT_LEVEL_START";
	public const string GOALS_OK_BUTTON_CLICKED = "GOALS_OK_BUTTON_CLICKED";
	public const string LEVEL_BUTTON_CLICKED = "LEVEL_BUTTON_CLICKED";
	public const string NEXT_LEVEL_BUTTON_CLICKED = "NEXT_LEVEL_BUTTON_CLICKED";
	public const string HAMBURGER_BUTTON_CLICKED = "HAMBURGER_BUTTON_CLICKED";
	public const string PAUSE_BUTTON_CLICKED = "PAUSE_BUTTON_CLICKED";
	public const string RESUME_BUTTON_CLICKED = "RESUME_BUTTON_CLICKED";
	public const string EVENT_BALL_SHOT = "EVENT_BALL_SHOT";
	public const string EVENT_BALL_DEATH = "EVENT_BALL_DEATH";
	public const string EVENT_BALL_EXIT = "EVENT_BALL_EXIT";
	public const string EVENT_BALL_EXIT_ENTERED = "EVENT_BALL_EXIT_ENTERED";
	public const string EVENT_LEVEL_FAILED = "EVENT_LEVEL_FAILED";
	public const string EVENT_BALLS_UPDATED = "EVENT_BALLS_UPDATED";
	public const string EVENT_OUT_OF_BALLS = "EVENT_OUT_OF_BALLS";
	public const string EVENT_OUT_OF_TIME = "EVENT_OUT_OF_TIME";
	public const string EVENT_MINIMUMSCORE_REACHED = "EVENT_MINIMUMSCORE_REACHED";
	public const string EVENT_MENU_SHOW = "EVENT_MENU_SHOW";
	public const string EVENT_MESSAGE_OK = "EVENT_MESSAGE_OK";
	public const string EVENT_ENDOFLEVEL_APPEARED = "EVENT_ENDOFLEVEL_APPEARED";
	public const string EVENT_ENDOFLEVEL_DISAPPEARED = "EVENT_ENDOFLEVEL_DISAPPEARED";
	public const string EVENT_QUIT = "EVENT_QUIT";
	public const string EVENT_COLLECTABLE_FOUND = "EVENT_COLLECTABLE_FOUND";
	public const string EVENT_ALL_COLLECTABLES_FOUND = "EVENT_ALL_COLLECTABLES_FOUND";
	public const string EVENT_EXIT_VISIBLE = "EVENT_EXIT_VISIBLE";
	public const string EVENT_SCORE_MULTIPLIER = "EVENT_SCORE_MULTIPLIER";
	public const string EVENT_SCORE_MULTIPLIER_END = "EVENT_SCORE_MULTIPLIER_END";
	public const string EVENT_SCORE_INCREASE = "EVENT_SCORE_INCREASE";
	public const string EVENT_LEVEL_INCREASE = "EVENT_LEVEL_INCREASE";
	public const string EVENT_LEVELMAP_PAGE_CHANGE = "EVENT_LEVELMAP_PAGE_CHANGE";
	public const string EVENT_TILT_START = "EVENT_TILT_START";
	public const string EVENT_TILT_END = "EVENT_TILT_END";
	public const string EVENT_ALL_COINS_COLLECTED = "EVENT_ALL_COINS_COLLECTED";
	public const string EVENT_EXTRA_BALL = "EVENT_EXTRA_BALL";
	
	

	public delegate void Callbackmethod(string customevent);
	public static EventManager instance = null;

	
	public EventManager() 
	{
		callbackMethods	= new List<Callbackmethod>();
		EventManager.instance = this;
	}
	
	public static void Subscribe(Callbackmethod incallback) 
	{
		if (EventManager.instance == null) new EventManager();
		EventManager.instance.callbackMethods.Add(incallback);
	}
	
	public static void UnSubscribe(Callbackmethod incallback) 
	{
		EventManager.instance.callbackMethods.Remove(incallback);
	}
	

	public static void fireEvent(string customevent) 
	{
		//Debug.Log("EVENT: " + customevent);
		
		//if (customevent.Equals(EventManager.EVENT_LEVEL_START)) Debug.Log("Eventmanager callbackMethods size: " + EventManager.instance.callbackMethods.Count);		
		foreach (Callbackmethod method in EventManager.instance.callbackMethods) 
		{
			method(customevent);	
		}
		
	}

	
	
}

