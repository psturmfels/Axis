using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTilt : MonoBehaviour {
	private RotationHolder rot;
	private float tiltIncrement = 1.0f;
	private float tiltDecrement = 2.0f;
	private float eps = 0.5f;
	private float maxTilt = 20.0f;
	private float xTilt = 0.0f;
	private float yTilt = 0.0f;


	void Start () {
		rot = GetComponent <RotationHolder> ();
	}
	
	void FixedUpdate () {
		Vector3 currentRotation = transform.rotation.eulerAngles;


		if (Mathf.Abs(rot.GetCurrentRotationSpeed ()) >= eps) {
			if (rot.GetCurrentRotationSpeed () < 0.0f) {
				if (yTilt < maxTilt) {
					transform.Rotate (Vector3.up * tiltIncrement);
					yTilt += tiltIncrement;
				}
			} else if (rot.GetCurrentRotationSpeed () > 0.0f) {
				if (yTilt > -maxTilt) {
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
			else {
				Quaternion yIdentity = new Quaternion (transform.rotation.x, Quaternion.identity.y, transform.rotation.z, transform.rotation.w);
				transform.rotation = yIdentity;
				yTilt = 0.0f;
			}
		}
	}
}
