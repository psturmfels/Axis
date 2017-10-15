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

	public void StartEnablePanel() {
		backgroundMusic.SetActive (false);
		Invoke ("EnablePanel", 4.0f);
	}

	void EnablePanel() {
		AudioSource.PlayClipAtPoint (gameOverJingle, Vector3.back * 500.0f, 0.3f);
		gameOverText.text = "Game Over\nYour Score:\n" + cts.GetCurrentScore ();
		gameOverPanel.SetActive(true);
	}
}
