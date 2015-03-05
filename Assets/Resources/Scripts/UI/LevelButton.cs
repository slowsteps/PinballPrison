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
		}
		else if (LevelNumber < GameManager.instance.currentLevel) 
		{
			iconSlot.GetComponent<Image>().sprite = completedIcon;
		}
		else 
		{
			iconSlot.GetComponent<Image>().sprite = lockedIcon;
		}
	}
	
	
}
