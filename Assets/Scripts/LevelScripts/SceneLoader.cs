using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public KeyCode resetKey;
	private float sceneLoadWaitTime = 4.0f; 

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (resetKey)) {
			SceneManager.LoadScene ("Main");
		}
	}

	public void StartLoadMainScene() {
		Invoke ("LoadMainScene", sceneLoadWaitTime);
	}

	void LoadMainScene() {
		SceneManager.LoadSceneAsync ("Main");
	}

	public void StartLoadTutorialScene() {
		Invoke ("LoadTutorialScene", sceneLoadWaitTime);
	}

	void LoadTutorialScene() {
		SceneManager.LoadSceneAsync ("Tutorial");
	}

}
