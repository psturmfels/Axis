using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
	public string sceneName;

	private Button button;

	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener (onClick);
	}
	
	void onClick() {
		if (GameObject.Find ("ResetLevel") != null) {
			if (sceneName == "Main") {
				GameObject.Find ("ResetLevel").GetComponent<SceneLoader> ().StartLoadMainScene ();
			} else if (sceneName == "Tutorial") {
				GameObject.Find ("ResetLevel").GetComponent<SceneLoader> ().StartLoadTutorialScene ();
			}
		}
	}
}
