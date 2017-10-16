using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableGameOverPanel : MonoBehaviour {
	public GameObject gameOverPanel;
	public Text gameOverText;
	public ComboTrackerScript cts;
	public GameObject backgroundMusic;
	public AudioClip gameOverJingle;
	public HighScoreText hst;

	public void StartEnablePanel() {
		backgroundMusic.SetActive (false);
		Invoke ("EnablePanel", 4.0f);
	}

	void EnablePanel() {
		PlayerPrefs.SetInt ("HighScore", cts.GetCurrentScore ());
		hst.updateScoreText ();
		AudioSource.PlayClipAtPoint (gameOverJingle, Vector3.back * 500.0f, 0.5f);
		gameOverText.text = "Game Over\nYour Score:\n" + cts.GetCurrentScore ();
		gameOverPanel.SetActive(true);
	}
}
