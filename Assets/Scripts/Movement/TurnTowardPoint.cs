using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardPoint : MonoBehaviour {
	public float rotationIncrementAbs = 5.0f;

	private bool isTurning = false;
	private float destinationTheta = 0.0f;
	private float incrementValue = 0.0f;
	private RotationHolder rot;
	private Vector3 localZAxis;

	public bool GetIsTurning() {
		return isTurning;
	}

	void Awake() {
		rot = GetComponent<RotationHolder> ();
		localZAxis = transform.forward;
	}

	void FixedUpdate() {
		if (isTurning) {
			if (Mathf.Abs (transform.rotation.eulerAngles.z - destinationTheta) < rotationIncrementAbs) {
				transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, destinationTheta);
				StopTurning ();
			} else {
				transform.RotateAround (transform.position, localZAxis, incrementValue);
			}
		}
	}
		
	public void StopTurning() {
		isTurning = false;
		rot.SetCurrentRotationSpeed (0.0f);
		if (GetComponent<InputManager> () != null) {
			GetComponent<InputManager> ().SetInputEnabled (true);
		}
	}

	public void StartTurningTowardPoint(Vector3 targetPoint) {
		float theta = -1.0f * Mathf.Rad2Deg * Mathf.Atan ((targetPoint.x - transform.position.x) / (targetPoint.y - transform.position.y));
		if (transform.position.y > targetPoint.y) {
			theta = theta - 180.0f;
		}

		if (theta < 0.0f) {
			theta = 360.0f + theta;
		}

		if (Mathf.Abs (theta - transform.rotation.eulerAngles.z) < 0.1f) {
			rot.SetCurrentRotationSpeed (0.0f);
			return;
		}

		if (GetComponent<InputManager> () != null) {
			InputManager im = GetComponent<InputManager> ();
			if (!im.GetInputEnabled ()) {
				return;
			}
			GetComponent<InputManager> ().SetInputEnabled (false);
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}

		if ((Mathf.Min(transform.rotation.eulerAngles.z, 360.0f) + 180.0f > theta &&
			transform.rotation.eulerAngles.z < theta) || 
			theta < Mathf.Max(transform.rotation.eulerAngles.z - 180.0f, 0.0f)
			) {
			incrementValue = rotationIncrementAbs;
		} else {
			incrementValue = -rotationIncrementAbs;
		}
		rot.SetCurrentRotationSpeed (-incrementValue);

		destinationTheta = theta;
		isTurning = true;
	}

	public void SnapTowardPoint (Vector3 targetPoint) {
		float theta = -1.0f * Mathf.Rad2Deg * Mathf.Atan ((targetPoint.x - transform.position.x) / (targetPoint.y - transform.position.y));
		if (transform.position.y > targetPoint.y) {
			theta = theta - 180.0f;
		}
		transform.rotation = Quaternion.Euler (Quaternion.identity.eulerAngles.x, Quaternion.identity.eulerAngles.y, theta);
	}
}
