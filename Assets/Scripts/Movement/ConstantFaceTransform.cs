using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFaceTransform : MonoBehaviour {
	public Transform target;
	private TurnTowardPoint ttp;

	void Start() {
		ttp = GetComponent<TurnTowardPoint> ();
	}

	void FixedUpdate() {
		if (!ttp.GetIsTurning ()) {
			ttp.StartTurningTowardPoint (target.position);
		}
	}
}
