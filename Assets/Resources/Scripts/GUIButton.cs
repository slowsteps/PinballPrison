using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {

	public buttonEnum myButton = buttonEnum.LEVEL1;
	public enum buttonEnum {LEVEL1,LEVEL2};

	
	
	public void OnMouseDown()
	{
		switch(myButton)
		{
		case buttonEnum.LEVEL1:
			LoadLevel("Level1_Prefab");
			break;
		case buttonEnum.LEVEL2:
			LoadLevel("Level2_Prefab");
			break;
		}
	
	}

	private void LoadLevel(string inLevelName)
	{
		Instantiate(Resources.Load("Prefabs/"+inLevelName));
	}
		
				
}
