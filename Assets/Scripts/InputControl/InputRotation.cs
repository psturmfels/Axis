using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour {
	private float rotEps = 0.6f;
	private float maxRotationSpeed = 5.0f;
	private float rotationSpeedIncrement = 2.5f;
	private float currentRotationSpeed = 0.0f;
	private float currentTurnAxis = 1.0f;
	private RotationHolder rot;
	private InputManager im;
	private Vector3 localZAxis;

	void Start() {
		rot = GetComponent<RotationHolder> ();
		im = GetComponent<InputManager> ();
		localZAxis = transform.forward;
	}

	void FixedUpdate() {
		float turnAxis = Input.GetAxisRaw ("Horizontal");
		if (im.GetInputEnabled () && Mathf.Abs (turnAxis) >= 0.2f) {
			currentTurnAxis = Mathf.Sign (turnAxis);
			rot.SetCurrentTurnAxis (currentTurnAxis);

			currentRotationSpeed += rotationSpeedIncrement * currentTurnAxis;
			currentRotationSpeed = Mathf.Min (currentRotationSpeed, maxRotationSpeed);
			currentRotationSpeed = Mathf.Max (currentRotationSpeed, -maxRotationSpeed);
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

			transform.RotateAround (transform.position, localZAxis, -currentRotationSpeed); 
		} 
		else if (Mathf.Abs (currentRotationSpeed) >= rotEps) {
			currentRotationSpeed = 0.0f;
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

//				transform.RotateAround (transform.position, localZAxis, -currentRotationSpeed); 
		}
	}
}
