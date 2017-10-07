using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttackGrow : MonoBehaviour {
	public GameObject circleResidual;
	public Vector3 growSpeeds;
	public Vector3 maxDims;


	// Update is called once per frame
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
		GameObject residualObject = Instantiate (circleResidual, transform.position, transform.rotation);
		residualObject.transform.localScale = transform.localScale;
	}
}
