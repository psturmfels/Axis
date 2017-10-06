using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTilt : MonoBehaviour {
	private RotationHolder rot;
	private float tiltIncrement = 1.0f;
	private float tiltDecrement = 2.5f;
	private float eps = 0.6f;
	private float maxTilt = 20.0f;
	private float yTilt = 0.0f;
	private bool isEnabled = false;

	void Start () {
		rot = GetComponent <RotationHolder> ();
	}

	public float GetYTilt() {
		return yTilt;
	}

	public bool GetIsEnabled() {
		return isEnabled;
	}

	public void RemoveTilt() {
		isEnabled = false;
		float zRotation = transform.rotation.eulerAngles.z; 
		transform.rotation = Quaternion.Euler (Quaternion.identity.eulerAngles.x, Quaternion.identity.eulerAngles.y, zRotation);
		yTilt = 0.0f;
	}
	
	void FixedUpdate () {
		if (Mathf.Abs(rot.GetCurrentRotationSpeed ()) >= eps) {
			if (rot.GetCurrentRotationSpeed () < 0.0f) {
				if (yTilt < maxTilt) {
					isEnabled = true;
					transform.Rotate (Vector3.up * tiltIncrement);
					yTilt += tiltIncrement;
				}
			} else if (rot.GetCurrentRotationSpeed () > 0.0f) {
				if (yTilt > -maxTilt) {
					isEnabled = true;
					transform.Rotate (Vector3.down * tiltIncrement);
					yTilt -= tiltIncrement;
				}
			}
		} else {
			if (yTilt > tiltDecrement) {
				transform.Rotate (Vector3.down * tiltDecrement);
				yTilt -= tiltDecrement;
			} else if (yTilt < -tiltDecrement) {
				transform.Rotate (Vector3.up * tiltDecrement);
				yTilt += tiltDecrement;
			}
			else if (isEnabled) {
				RemoveTilt ();
			}
		}
	}
}
