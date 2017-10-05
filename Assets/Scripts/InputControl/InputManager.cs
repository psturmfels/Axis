using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	private bool inputEnabled = true;

	public bool GetInputEnabled() {
		return inputEnabled;
	}

	public void SetInputEnabled(bool newInputEnabled) {
		inputEnabled = newInputEnabled;
	}
}
