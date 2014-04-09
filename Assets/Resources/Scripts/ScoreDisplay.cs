using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	private int displayScore;


	void Update () {
		displayScore = GameManager.instance.score;
		guiText.text = displayScore + "/" + Level.instance.minimumScore;		
	}
}
