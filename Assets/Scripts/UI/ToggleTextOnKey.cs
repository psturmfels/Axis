using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTextOnKey : MonoBehaviour {
	public KeyCode ToggleKey;
	private Text textObject;

	void Start () {
		textObject = GetComponent<Text> ();	
	}
	
	void Update () {
		if (Input.GetKeyDown (ToggleKey)) {
			textObject.enabled = !textObject.enabled;
		}
	}
}
