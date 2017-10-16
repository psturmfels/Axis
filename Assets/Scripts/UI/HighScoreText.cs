using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {
	private ComboTrackerScript cts;
	private int bestHighScore;
	private Text textObject;

	void Start () {
		textObject = GetComponent<Text> ();
		updateScoreText ();
	}

	public void updateScoreText() {
		bestHighScore = PlayerPrefs.GetInt ("HighScore");
		textObject.text = "Your High Score:\n" + bestHighScore.ToString ();
	}

}
