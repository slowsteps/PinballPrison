using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {

	public buttonEnum myButton = buttonEnum.LEVEL;
	public enum buttonEnum {LEVEL,START,MENU};
	public int levelNumber=1;

	
	
	public void OnMouseDown()
	{
	
		Settings.hasPlayerClicked = true;
	
		switch(myButton)
		{
		case buttonEnum.LEVEL:
			LoadLevel(levelNumber);
			break;
		case buttonEnum.MENU:
			EventManager.fireEvent(EventManager.EVENT_OUT_OF_BALLS);
			break;
		}
	
	}

	private void LoadLevel(int inLevelNum)
	{
		if (GameManager.instance.lives > 0)
		{
			GameObject go = Instantiate(Resources.Load("Prefabs/Level"+inLevelNum+"_Prefab")) as GameObject;
			go.name ="Loaded level number " + inLevelNum;
			EventManager.fireEvent(EventManager.EVENT_LEVEL_START);
		}
		else print ("out of lives");
	}
		
				
}
