using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {
	private ComboTrackerScript cts;
	private int bestHighScore;
	private Text textObject;

	void Awake () {
		textObject = GetComponent<Text> ();
		updateScoreText ();
	}

	public void updateScoreText() {
		bestHighScore = PlayerPrefs.GetInt ("HighScore");
	}

	void Update() {
		if (textObject != null) {
			textObject.text = "Your High Score:\n" + bestHighScore.ToString ();
		}
	}
}
