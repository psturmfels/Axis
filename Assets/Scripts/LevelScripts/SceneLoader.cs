using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public KeyCode resetKey;
	public GameObject fadePanel;
	private float sceneLoadWaitTime = 1.0f; 

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (resetKey)) {
			LoadTutorialScene ();
		}
	}

	public void StartLoadMainScene() {
		Invoke ("LoadMainScene", sceneLoadWaitTime);
		fadePanel.SetActive (true);
		fadePanel.GetComponent<FadePanelScript> (). StartFadeIn ();
	}

	void LoadMainScene() {
		SceneManager.LoadSceneAsync ("Main");
	}

	public void StartLoadTutorialScene() {
		fadePanel.SetActive (true);
		fadePanel.GetComponent<FadePanelScript> (). StartFadeIn ();
		Invoke ("LoadTutorialScene", sceneLoadWaitTime);
	}

	void LoadTutorialScene() {
		SceneManager.LoadSceneAsync ("Tutorial");
	}

}
