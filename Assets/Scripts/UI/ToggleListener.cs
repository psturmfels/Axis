using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleListener : MonoBehaviour {
	public KeyCode audioToggle;
	private AudioListener al;

	void Start () {
		al = GetComponent<AudioListener> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (audioToggle)) {
			al.enabled = !al.enabled;
		}
	}
}
