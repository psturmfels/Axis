using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMotion : MonoBehaviour {
	public Transform target;
	public float strideDistance;
	public float timeBetweenStrides;

	private Rigidbody rb;
	private Quaternion startRotation;
	private float sphereRadius = 1250.0f;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		transform.rotation = Quaternion.Euler (90.0f, 0.0f, 0.0f);
		startRotation = transform.rotation; 
		if (target == null) {
			if (GameObject.FindGameObjectWithTag ("Player") == null) {
				return; 
			}
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		Stride ();
	}

	void FixedUpdate () {
		if (transform.rotation != startRotation) {
			transform.rotation = Quaternion.Lerp (transform.rotation, startRotation, 0.1f);
		}
	}

	void Stride () {
		if (target == null) {
			return;
		}

		Collider[] nearbyColliders = Physics.OverlapSphere (transform.position, sphereRadius);
		foreach (Collider coll in nearbyColliders) {
			if ( 	(coll.gameObject.GetComponent<KillEnemyOnContact> () != null &&
					coll.gameObject.GetComponent<KillEnemyOnContact> ().isEnabled) ||
					coll.gameObject.CompareTag ("Projection") || 
					coll.gameObject.CompareTag("AttackIndicator")) {
				Vector3 attackPosition = coll.gameObject.transform.position;
				moveTowardPosition (attackPosition, true, 2.0f);
				Invoke ("Stride", timeBetweenStrides);
				return;
			}
		}

		moveTowardPosition (target.position);
		Invoke ("Stride", timeBetweenStrides);
	}

	void moveTowardPosition(Vector3 targetPosition, bool invert = false, float augmentation = 1.0f) {
		Vector3 difference = targetPosition - transform.position;
		if (invert) {
			difference = -difference;
		}

		if (Mathf.Abs (difference.x) > Mathf.Abs (difference.y)) {
			transform.RotateAround (transform.position, Mathf.Sign (difference.x) * transform.forward, 30.0f);

			Vector3 forceVector = Vector3.right * Mathf.Sign (difference.x) * strideDistance * augmentation;
			rb.AddForce (forceVector, ForceMode.Impulse);
		} else {
			transform.RotateAround (transform.position, Mathf.Sign (difference.y) * transform.right, 30.0f);

			Vector3 forceVector = Vector3.up * Mathf.Sign (difference.y) * strideDistance * augmentation;
			rb.AddForce (forceVector, ForceMode.Impulse);
		}
	}
}
