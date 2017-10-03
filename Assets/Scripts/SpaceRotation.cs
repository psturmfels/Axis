using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRotation : MonoBehaviour {
	void Start() {
	}
		
	void FixedUpdate() {
		transform.RotateAround (Vector3.zero, new Vector3 (0.0f, 0.0f, 1.0f), 10.0f * Time.deltaTime);
	}
}
