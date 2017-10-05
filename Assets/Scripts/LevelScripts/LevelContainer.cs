using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour {
	public Rigidbody playerBody;
	public Transform playerTransform;
	public SpawnGhostShip spawner;

	private float containHorzBoundary = 3700.0f;
	private float containVertBoundary = 1700.0f;
	private bool playerHasLeft;
	private float playerMass;

	void Start() {
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (playerHasLeft) {
			float gravityForce = -1.0f * 6.0f;
			if (playerTransform.position.y >= containVertBoundary) {
				playerBody.AddExplosionForce (gravityForce * playerBody.position.y, playerTransform.position + Vector3.down * 500.0f, float.MaxValue, 0.0f, ForceMode.Force);
			} else if (playerTransform.position.y <= -containVertBoundary) {
				playerBody.AddExplosionForce (gravityForce * playerBody.position.y, playerTransform.position + Vector3.down * 500.0f, float.MaxValue, 0.0f, ForceMode.Force);
			}

			gravityForce = -1.0f * 3.3f;
			if (playerTransform.position.x >= containHorzBoundary) {
				playerBody.AddExplosionForce (gravityForce * playerBody.position.x, playerTransform.position + Vector3.left * 500.0f, float.MaxValue, 0.0f, ForceMode.Force);
			} else if (playerTransform.position.x <= -containHorzBoundary) {
				playerBody.AddExplosionForce (gravityForce * playerBody.position.x, playerTransform.position + Vector3.left * 500.0f, float.MaxValue, 0.0f, ForceMode.Force);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			playerHasLeft = true;
			spawner.DisableSpawn ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			playerHasLeft = false;
			spawner.EnableSpawn ();
		}
	}
}
