using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour {
	private float eps = 0.05f;
	private float maxRotationSpeed = 1.0f;
	private float rotationSpeedIncrement = 0.03f;
	private float rotationSpeedDecrement = 0.02f;
	private float currentRotationSpeed = 0.0f;
	private float currentTurnAxis = 1.0f;
	private RotationHolder rot;

	void Start() {
		rot = GetComponent<RotationHolder> ();
	}

	void FixedUpdate() {
		float turnAxis = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (turnAxis) >= eps) {
			currentTurnAxis = Mathf.Sign (turnAxis);
			rot.SetCurrentTurnAxis (currentTurnAxis);

			currentRotationSpeed += rotationSpeedIncrement;
			currentRotationSpeed = Mathf.Min (currentRotationSpeed, maxRotationSpeed);
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

			transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f * currentRotationSpeed) * currentTurnAxis);
		} else if (Mathf.Abs (currentRotationSpeed) >= eps) {
			currentRotationSpeed -= rotationSpeedDecrement;
			currentRotationSpeed = Mathf.Max (currentRotationSpeed, 0.0f);
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

			transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f * currentRotationSpeed) * currentTurnAxis);
		}
	}
}
