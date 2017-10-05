using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour {
	private float rotEps = 0.6f;
	private float maxRotationSpeed = 8.0f;
	private float rotationSpeedIncrement = 0.8f;
//	private float rotationSpeedDecrement = 1.5f;
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
//			transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f * currentRotationSpeed), Space.World);
		} 
		else if (Mathf.Abs (currentRotationSpeed) >= rotEps) {
//			if (Mathf.Sign(currentRotationSpeed - rotationSpeedDecrement * currentTurnAxis) != Mathf.Sign(currentRotationSpeed)) {
//				currentRotationSpeed = 0.0f;
//			} else {
//				currentRotationSpeed -= rotationSpeedDecrement * currentTurnAxis;
//			}
			currentRotationSpeed = 0.0f;
			currentRotationSpeed = Mathf.Min (currentRotationSpeed, maxRotationSpeed);
			currentRotationSpeed = Mathf.Max (currentRotationSpeed, -maxRotationSpeed);
			rot.SetCurrentRotationSpeed (currentRotationSpeed);

			transform.RotateAround (transform.position, localZAxis, -currentRotationSpeed); 
//			transform.Rotate (new Vector3 (0.0f, 0.0f, -1.0f * currentRotationSpeed), Space.World);
		}

//		float vertAxis = Input.GetAxis("Vertical");
//		if (Mathf.Abs (vertAxis) >= eps) {
//			transform.Rotate (Vector3.right * vertAxis * 0.5f);
//		}
	}
}
