using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour {
	private float eps = 0.5f;
	private float maxRotationSpeed = 6.0f;
	private float rotationSpeedIncrement = 1.0f;
	private float rotationSpeedDecrement = 0.4f;
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

			currentRotationSpeed += rotationSpeedIncrement * currentTurnAxis;
			currentRotationSpeed = Mathf.Min (currentRotationSpeed, maxRotationSpeed);
			currentRotationSpeed = Mathf.Max (currentRotationSpeed, -maxRotationSpeed);
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

			transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f * currentRotationSpeed), Space.World);
		} 
		else if (Mathf.Abs (currentRotationSpeed) >= eps) {
			currentRotationSpeed -= rotationSpeedDecrement * currentTurnAxis;
			currentRotationSpeed = Mathf.Min (currentRotationSpeed, maxRotationSpeed);
			currentRotationSpeed = Mathf.Max (currentRotationSpeed, -maxRotationSpeed);
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

			transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f * currentRotationSpeed), Space.World);
		}

//		float vertAxis = Input.GetAxis("Vertical");
//		if (Mathf.Abs (vertAxis) >= eps) {
//			transform.Rotate (Vector3.right * vertAxis * 0.5f);
//		}
	}
}
