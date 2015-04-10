using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//this is the main menu with the level buttons, logo etc.

public class GUIMenu : MonoBehaviour {

	public static GUIMenu instance;
	public GameObject logo;
	public GameObject levelButton;
	public GameObject gridLayout;
	public int numLevels=40;
	public int pageSize=20;
	public int page=0;
	
	

	void Start()
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
		EventManager.fireEvent(EventManager.EVENT_GAME_START);
	}
	
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_QUIT:
			Show();
			break;
		case EventManager.EVENT_GAME_START:
			MakeLevelButtons();
			SoundManager.instance.PlaySound("MenuSong",true);
			break;
		case EventManager.EVENT_LEVEL_START:
			Hide();
			break;
		case EventManager.LEVEL_BUTTON_CLICKED:
			//Hide();
			break;			
		}
	}
	
	
	
	
	public void Show()
	{
		gameObject.SetActive(true);
		EventManager.fireEvent(EventManager.EVENT_MENU_SHOW);
		SoundManager.instance.PlaySound("MenuSong",true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
	
	private void MakeLevelButtons()
	{
		page = Mathf.FloorToInt(GameManager.instance.currentLevel / pageSize);
		int topleftNumber = pageSize*page;
		int sequenceNumber = 1;
		
		for (int i=topleftNumber;i<topleftNumber+pageSize;i++)
		{
			GameObject lb = GameObject.Instantiate(levelButton);
			//lb.transform.parent = gridLayout.transform;
			lb.transform.SetParent(gridLayout.transform,false);
			lb.transform.localScale = new Vector3(1f,1f,1f);
			lb.GetComponent<LevelButton>().LevelNumber = i;
			lb.GetComponent<LevelButton>().sequenceNumber = sequenceNumber;
			sequenceNumber++;
		}
	}
	
	public void PageUp()
	{
		//TODO check upperbounds
		page++;
		EventManager.fireEvent(EventManager.EVENT_LEVELMAP_PAGE_CHANGE);
	}
	
	public void PageDown()
	{
		if (page > 0) 
		{
			page--;
		}
		EventManager.fireEvent(EventManager.EVENT_LEVELMAP_PAGE_CHANGE);
	}
			
}
