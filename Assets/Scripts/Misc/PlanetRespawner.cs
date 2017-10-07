using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRespawner : MonoBehaviour {
	private float boundingFloat = 7000.0f;
	private float spawnXMin = 4500.0f;
	private float spawnYMin = 2500.0f;
	private float fixedZPos = 200.0f;
	private float velocityInitMul = 100.0f;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		if (Mathf.Abs (transform.position.x) > boundingFloat ||
		    Mathf.Abs (transform.position.y) > boundingFloat) {
			float newXPos = Random.Range (spawnXMin, boundingFloat);
			float newYPos = Random.Range (spawnYMin, boundingFloat);
			if (Random.Range (0, 2) == 0) {
				newXPos = -newXPos;
			}
			if (Random.Range (0, 2) == 0) {
				newYPos = -newYPos;
			}
			transform.position = new Vector3 (newXPos, newYPos, fixedZPos);
			Vector2 initVelocity = Random.insideUnitCircle;
			rb.velocity = new Vector3 (initVelocity.x, initVelocity.y, 0.0f) * velocityInitMul;
		}
	}
}
