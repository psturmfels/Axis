using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	private bool inputEnabled = true;
	private bool forwardMotionEnabled = true;

	public bool GetForwardMotionEnabled() {
		return forwardMotionEnabled;
	}

	public void SetForwardMotionEnabled(bool newForwardMotionEnabled) {
		forwardMotionEnabled = newForwardMotionEnabled;
	}

	public bool GetInputEnabled() {
		return inputEnabled;
	}

	public void SetInputEnabled(bool newInputEnabled) {
		inputEnabled = newInputEnabled;
	}
}
