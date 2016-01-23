using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIEndOfLevel : MonoBehaviour {
	
	public static GUIEndOfLevel instance;
	public Text textField;
	public GameObject SuccessHeader;
	public GameObject FailedHeader;
	public GameObject TryAgainButton;
	public GameObject NextLevelButton;
	public GameObject MenuButton;
	public GameObject VideoAdButton;
	public GameObject ExtraBallCountdownLabel;
	
	
	void Start () 
	{
		instance = this;
		EventManager.Subscribe(OnEvent);
		SuccessHeader.SetActive(false);
		FailedHeader.SetActive(false);
		
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_GAME_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_BALL_EXIT:
			gameObject.SetActive(true);
			TryAgainButton.SetActive(false);
			SuccessHeader.SetActive(true);
			FailedHeader.SetActive(false);
			if (GameManager.instance.NextLevelIsAvailable()) NextLevelButton.SetActive(true);
			else NextLevelButton.SetActive(false);
			gameObject.GetComponent<Animator>().SetTrigger("isShow");
			break;
		case EventManager.EVENT_LEVEL_FAILED:
			gameObject.SetActive(true);
			if (GameManager.instance.balls > 0) TryAgainButton.SetActive(true);
			else TryAgainButton.SetActive(false);
			SuccessHeader.SetActive(false);
			FailedHeader.SetActive(true);
			NextLevelButton.SetActive(false);
			SoundManager.instance.PlaySound("LevelFail_SFX");
			break;
		case EventManager.EVENT_EXTRA_BALL:
			TryAgainButton.SetActive(true);
			VideoAdButton.SetActive(false);
			ExtraBallCountdownLabel.SetActive(false);
			break;
		case EventManager.EVENT_OUT_OF_BALLS:
			TryAgainButton.SetActive(false);
			VideoAdButton.SetActive(true);
			ExtraBallCountdownLabel.SetActive(true);
			break;
			
		}
	}
	
	public void OnMenuButton()
	{
		EventManager.fireEvent(EventManager.EVENT_QUIT);
		SoundManager.instance.PlaySound("Select_SFX");
		gameObject.SetActive(false);
	}

	public void OnTryAgainButton()
	{
		GameManager.instance.LoadGameLevel(GameManager.instance.loadedLevel);
	
		SoundManager.instance.PlaySound("Select_SFX");
		gameObject.SetActive(false);
	}
	
	public void OnNextLevelButton()
	{
		GameManager.instance.LoadNextGameLevel();
		SoundManager.instance.PlaySound("Select_SFX");
		EventManager.fireEvent(EventManager.NEXT_LEVEL_BUTTON_CLICKED);
		gameObject.SetActive(false);
	}
	
			
									
	public void OnHideAnimComplete()
	{
		EventManager.fireEvent(EventManager.EVENT_MESSAGE_OK);
		gameObject.SetActive(false);
	}
	
	public void OnAppearAnimComplete()
	{
		EventManager.fireEvent(EventManager.EVENT_ENDOFLEVEL_APPEARED);
	}
	
	public void OnDisappearAnimComplete()
	{
		
	}
	
			
	public void SetMessage(string inText)
	{
		gameObject.SetActive(true);
		textField.text = inText;
	}
	
	
}

