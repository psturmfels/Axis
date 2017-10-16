using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleListener : MonoBehaviour {
	public KeyCode audioToggle;

	void Update () {
		if (Input.GetKeyDown (audioToggle)) {
			AudioListener.pause = !AudioListener.pause;
		}
	}
}
