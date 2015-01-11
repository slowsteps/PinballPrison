using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	public LevelThemes CurLevelTheme;
	public levelStates CurLevelState;
	public Image ThemeImage;
	public Text LevelNumberLabel;
	public int LevelNumber=1;
	//public 
	
	public enum LevelThemes {Square,Diamond,Hexagon,Circle};
	public enum levelStates {Locked,Playable,MinimalOneChallengeDone,AllChallengesDone};

	// Use this for initialization
	void Start () 
	{
		//LevelNumberLabel.text = LevelNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
