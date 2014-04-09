using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class EventManager {
	
	private List<Callbackmethod> callbackMethods; 

	public const string EVENT_LEVEL_START = "EVENT_LEVEL_START";
	public const string EVENT_BALL_DEATH = "EVENT_BALL_DEATH";
	public const string EVENT_BALL_EXIT = "EVENT_BALL_EXIT";
	public const string EVENT_BALLS_UPDATED = "EVENT_BALLS_UPDATED";
	

	public delegate void Callbackmethod(string customevent);
	public static EventManager instance = null;

	
	public EventManager() {
		callbackMethods	= new List<Callbackmethod>();
		EventManager.instance = this;
	}
	
	public static void Subscribe(Callbackmethod incallback) {
		if (EventManager.instance == null) new EventManager();
		EventManager.instance.callbackMethods.Add(incallback);
	}
	

	public static void fireEvent(string customevent) {
		
		foreach (Callbackmethod method in EventManager.instance.callbackMethods) {
			method(customevent);	
		}
		
	}

	
	
}

