using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFaceTransform : MonoBehaviour {
	public Transform target;
	private TurnTowardPoint ttp;

	void Start() {
		ttp = GetComponent<TurnTowardPoint> ();
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	void FixedUpdate() {
		if (!ttp.GetIsTurning ()) {
			ttp.StartTurningTowardPoint (target.position);
		}
	}
}
