using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFlip : MonoBehaviour {
	private RotationTilt rt;
	private InputManager im;
	private bool isFlipping = false;
	private bool canFlip = true;
	private float destinationX = 180.0f;
	private float tiltIncrement = 20.0f;
	private float xTilt = 0.0f;

	void Start () {
		rt = GetComponent<RotationTilt> ();
		im = GetComponent<InputManager> ();
	}
	
	void Update () {
		if (canFlip && im.GetInputEnabled() && Input.GetKeyDown(KeyCode.Q)) {
			StartFlip ();
		}
	}

	void FixedUpdate () {
		if (isFlipping && rt.GetYTilt () == 0.0f) {
			if (xTilt == destinationX) {				
				xTilt = 0.0f;
				isFlipping = false;
				im.SetInputEnabled (true);
				transform.RotateAround (transform.position, transform.up, 180.0f);
				Invoke ("EnableFlip", 0.5f);
			} else {
				transform.RotateAround (transform.position, transform.right, -tiltIncrement);
				xTilt = xTilt + tiltIncrement;
			}
		}
	}

	void StartFlip () {
		canFlip = false;
		isFlipping = true;
		im.SetInputEnabled (false);
		rt.RemoveTilt ();
	}

	void EnableFlip () {
		canFlip = true;
	}
}
