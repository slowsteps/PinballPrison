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
		}
	
	}

	private void LoadLevel(int inLevelNum)
	{
		if (GameManager.instance.lives > 0)
		{
			Application.LoadLevelAdditive("Level"+inLevelNum);
			
		}
		else print ("out of lives");
	}
		
				
				
}
