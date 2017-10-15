using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseInput : MonoBehaviour {
	public KeyCode pauseKey;
	private bool isPaused = false;

	public InputManager im;
	public GameObject pausePanel;
	
	void Update () {
		if (Input.GetKeyDown (pauseKey)) {
			if (isPaused) {
				if (im != null) {
					im.SetInputEnabled (true);
				}
				pausePanel.SetActive (false);
				isPaused = false;
				Time.timeScale = 1.0f;
			} else {
				if (im != null) {
					im.SetInputEnabled (false);
				}
				pausePanel.SetActive (true);
				isPaused = true;
				Time.timeScale = 0.0f;
			}
		}
	}
}
