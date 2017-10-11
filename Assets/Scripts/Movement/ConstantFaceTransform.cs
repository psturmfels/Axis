using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFaceTransform : MonoBehaviour {
	public Transform target;
	private TurnTowardPoint ttp;

	void Start() {
		ttp = GetComponent<TurnTowardPoint> ();
		if (target == null) {
			if (GameObject.FindGameObjectWithTag ("Player") == null) {
				return; 
			}
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	void FixedUpdate() {
		if (target == null) {
			return; 
		}
		if (!ttp.GetIsTurning ()) {
			ttp.StartTurningTowardPoint (target.position);
		}
	}
}
