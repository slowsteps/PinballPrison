using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AllCoinsCollected : MonoBehaviour {

	public Text Label;

	// Use this for initialization
	void Start () 
	{
		EventManager.Subscribe(OnEvent);
		gameObject.SetActive(false);
	}
	
	public void OnEvent(string customEvent)
	{
		switch(customEvent)
		{
		case EventManager.EVENT_LEVEL_START:
			gameObject.SetActive(false);
			break;
		case EventManager.EVENT_ALL_COINS_COLLECTED:
			gameObject.SetActive(true);
			Label.text = "All " + CoinPickup.coinList.Count + " coins collected!";
			Invoke("Hide",3f);
			break;
		}
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
		

}
