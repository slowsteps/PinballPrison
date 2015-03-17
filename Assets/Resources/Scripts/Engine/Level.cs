using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {


	
	
	public static Level instance;
	
	
	
	
	[Header("Level settings for each individual level")]
	[Space(5)]
	
	[Tooltip("shown in tickertape")]
	[TextArea(0,5)]
	public string Description = "The best level description ever";
	
	[Tooltip("shown in end of level")]
	[TextArea(0,5)]
	public string FailMessage = "Find the exit and progress you will";
	
	[Tooltip("shown in end of level")]
	[TextArea(0,5)]
	public string SuccesMessage = "That's it! ";
	
	[HideInInspector]
	public string LongDescription = "";
	
	public bool hasMinScore = false;
	[HideInInspector]
	public bool hasMaxShots = false;
	public bool hasMaxTime = false;
	public bool hasCollectables = false;
	public int requiredScore = 0;
	[HideInInspector]
	public int allowedShots = 0;
	public float allowedTime = 0;
	public int requiredCollectables = 0;
	[HideInInspector]
	public int collectables = 0;
			
	void Awake () {
		
		instance = this;
		
		//a level is open in the editor
		if (!GameManager.instance)
		{
			Application.LoadLevelAdditive("Main");
		}
		//TODO can't remember why this is needed
		if (!Settings.hasPlayerClicked)
		{
			GameObject.Destroy(gameObject);
			gameObject.SetActive(false);
		}
		
		LongDescription = Description;
		if (hasMinScore) LongDescription += " - Goal: " + requiredScore + " Points";
		if (hasCollectables) LongDescription += " - Goal: " + requiredCollectables + " Keys";
		if (hasMaxShots) LongDescription += " - Max Shots: " + allowedShots;
		if (hasMaxTime) LongDescription += " - Max Time: " + allowedTime;
		
		EventManager.Subscribe(OnEvent);
	
		
	}
	
	void Start()
	{
		EventManager.fireEvent(EventManager.EVENT_LEVEL_START);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_QUIT:
			iTween.Stop(); // kill all mushroom anims etc.
			GameObject.Destroy(gameObject);
			break;
		case EventManager.EVENT_LEVEL_FAILED:
			iTween.Stop(); // kill all mushroom anims etc.
			GameObject.Destroy(gameObject);
			break;
		case EventManager.EVENT_COLLECTABLE_FOUND:
			IncreaseCollectables();
			break;
		}
	}
	
	private void IncreaseCollectables()
	{
		collectables++;
		if (collectables == Level.instance.requiredCollectables) 
		{
			EventManager.fireEvent(EventManager.EVENT_ALL_COLLECTABLES_FOUND);
			
		}
	}		
	
	void OnDestroy()
	{
		EventManager.UnSubscribe(OnEvent);
	}
	
	[ContextMenu("Test")]
	public void Test() 
	{
		Debug.Log("arataert");
	}
	
}
