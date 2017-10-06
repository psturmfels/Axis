using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardPoint : MonoBehaviour {
	public GameObject redCube;
	public float rotationIncrementAbs = 5.0f;

	private bool isTurning = false;
	private float destinationTheta = 0.0f;
	private float incrementValue = 0.0f;
	private RotationHolder rot;

	void Start() {
		rot = GetComponent<RotationHolder> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.W)) {
			StartTurningTowardPoint (redCube.transform.position);
		}
	}

	void FixedUpdate() {
		if (isTurning) {
			if (Mathf.Abs (transform.rotation.eulerAngles.z - destinationTheta) < rotationIncrementAbs) {
				transform.rotation = Quaternion.Euler (Quaternion.identity.eulerAngles.x, Quaternion.identity.eulerAngles.y, destinationTheta);
				StopTurning ();
			} else {
				transform.Rotate (new Vector3 (0.0f, 0.0f, incrementValue));
			}
		}
	}
		
	public void StopTurning() {
		isTurning = false;
		rot.SetCurrentRotationSpeed (0.0f);
		if (GetComponent<RotationTilt> () != null) {
			GetComponent<RotationTilt> ().RemoveTilt ();
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
