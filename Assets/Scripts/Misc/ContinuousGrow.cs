using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousGrow : MonoBehaviour {
	public Vector3 growDirections;
	public Vector3 growSpeeds;
	public Vector3 maxDims = Vector3.zero;
	
	void FixedUpdate () {
		if (maxDims != Vector3.zero) {
			if (transform.localScale.x > maxDims.x ||
				transform.localScale.y > maxDims.y ||
			    transform.localScale.z > maxDims.z) {
				maxDims = Vector3.zero;
				Destroy (gameObject);
			}
		}
		transform.localScale += growSpeeds;
	}
}
