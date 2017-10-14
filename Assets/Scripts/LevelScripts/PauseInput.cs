using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseInput : MonoBehaviour {
	public KeyCode pauseKey;
	private bool isPaused = false;

	public GameObject pausePanel;
	
	void Update () {
		if (Input.GetKeyDown (pauseKey)) {
			if (isPaused) {
				pausePanel.SetActive (false);
				isPaused = false;
				Time.timeScale = 1.0f;
			} else {
				pausePanel.SetActive (true);
				isPaused = true;
				Time.timeScale = 0.0f;
			}
		}
	}
}
