using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour {
	public Rigidbody playerBody;
	public Transform playerTransform;
	public SpawnGhostShip spawner;

	private float screenVertBoundary = 2550.0f;
	private float screenHorzBoundary = 4550.0f;
	private float containHorzBoundary = 3700.0f;
	private float containVertBoundary = 1700.0f;
	private bool playerHasLeft;
	private float playerMass;
	private int containerFlagIndex = 4;

	void FixedUpdate () {
		if (playerBody == null) {
			return;
		}

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
		if (Mathf.Abs (playerTransform.position.y) > screenVertBoundary) {
			playerHasLeft = true;
			playerTransform.position = new Vector3 (playerTransform.position.x, -screenVertBoundary * Mathf.Sign(playerTransform.position.y), playerTransform.position.z);
		}
		if (Mathf.Abs (playerTransform.position.x) > screenHorzBoundary) {
			playerHasLeft = true;
			playerTransform.position = new Vector3 (-screenHorzBoundary * Mathf.Sign(playerTransform.position.x), playerTransform.position.y, playerTransform.position.z);
		}
	}

	void OnTriggerExit(Collider other) {
		if (playerBody == null) {
			return;
		}
		if (other.gameObject.CompareTag ("Player")) {
			playerHasLeft = true;
			spawner.DisableSpawn ();
			if (GameObject.Find("TutorialPanel") != null) {
				TutorialObserver to = GameObject.Find("TutorialPanel").GetComponent<TutorialObserver> ();
				to.InitiateFlagCall (containerFlagIndex);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (playerBody == null) {
			return;
		}
		if (other.gameObject.CompareTag ("Player")) {
			playerHasLeft = false;
			spawner.EnableSpawn ();
		}
	}
}
