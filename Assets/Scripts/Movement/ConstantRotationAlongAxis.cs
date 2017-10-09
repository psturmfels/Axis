using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotationAlongAxis : MonoBehaviour {
	public Vector3 rotationAxis;
	public float rotationIncrement;

	void FixedUpdate  () {
		if (rotationAxis == Vector3.up) {
			transform.RotateAround (transform.position, transform.up, rotationIncrement);
		} else if (rotationAxis == Vector3.right) {
			transform.RotateAround (transform.position, transform.right, rotationIncrement);
		} else if (rotationAxis == Vector3.forward) {
			transform.RotateAround (transform.position, transform.forward, rotationIncrement);
		}
	}
}
