using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour {
	private float rotEps = 0.6f;
	private float maxRotationSpeed = 8.0f;
	private float rotationSpeedIncrement = 0.8f;
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
		float turnAxis = Input.GetAxis ("Horizontal");
		if (im.GetInputEnabled () && Mathf.Abs (turnAxis) >= 0.1f) {
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

			transform.RotateAround (transform.position, localZAxis, -currentRotationSpeed); 
		}
	}
}
