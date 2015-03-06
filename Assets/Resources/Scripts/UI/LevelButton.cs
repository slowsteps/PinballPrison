using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	
	public GameObject iconSlot;
	public Sprite lockedIcon;
	public Sprite openIcon;
	public Sprite completedIcon;
	public int LevelNumber;
	

	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.GetComponentInChildren<Text>().text = LevelNumber.ToString("00");
		UpdateIcon();
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			UpdateIcon();
			break;
		case EventManager.EVENT_LEVEL_INCREASE:
			UpdateIcon();	
			break;
		}
	}
	
	
	private void UpdateIcon()
	{
		if (LevelNumber == GameManager.instance.currentLevel) 
		{
			iconSlot.GetComponent<Image>().sprite = openIcon;
			gameObject.GetComponent<Button>().enabled = true;
		}
		else if (LevelNumber < GameManager.instance.currentLevel) 
		{
			iconSlot.GetComponent<Image>().sprite = completedIcon;
			gameObject.GetComponent<Button>().enabled = true;
		}
		else 
		{
			iconSlot.GetComponent<Image>().sprite = lockedIcon;
			gameObject.GetComponent<Button>().enabled = false;
		}
	}
	
	public void OnClick()
	{
		Settings.hasPlayerClicked = true;
		if (Application.CanStreamedLevelBeLoaded("Level"+LevelNumber)) 
		{
			Application.LoadLevelAdditive("Level"+LevelNumber);
			EventManager.fireEvent(EventManager.LEVEL_BUTTON_CLICKED);
			SoundManager.instance.PlaySound("Select_SFX");
		}
		else print("can't load scene Level"+LevelNumber) ;
	}
	
	
}
