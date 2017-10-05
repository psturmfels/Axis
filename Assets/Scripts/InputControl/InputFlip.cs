using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFlip : MonoBehaviour {
	private RotationTilt rt;
	private InputManager im;
	private bool isFlipping = false;
	private bool canFlip = true;
	private float destinationX = 180.0f;
	private float tiltIncrement = 15.0f;
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
				Invoke ("EnableFlip", 0.5f);
//				transform.Rotate (new Vector3 (0.0f, 0.0f, transform.rotation.z + 180.0f));
			} else {
				transform.Rotate (new Vector3(0.0f, 0.0f, 1.0f) * tiltIncrement, Space.World);
				xTilt = xTilt + tiltIncrement;
			}
		}
	}

	void StartFlip () {
		canFlip = false;
		isFlipping = true;
		im.SetInputEnabled (false);
	}

	void EnableFlip () {
		canFlip = true;
	}
}
