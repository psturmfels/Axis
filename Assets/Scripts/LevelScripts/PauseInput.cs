using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInput : MonoBehaviour {
	public KeyCode pauseKey;
	private bool isPaused = false;
	
	void Update () {
		if (Input.GetKeyDown (pauseKey)) {
			if (isPaused) {
				isPaused = false;
				Time.timeScale = 1.0f;
			} else {
				isPaused = true;
				Time.timeScale = 0.0f;
			}
		}
	}
}
