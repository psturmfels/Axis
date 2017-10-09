using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpNearTarget : MonoBehaviour {
	public Transform target;
	public float minSpeed;
	public float maxSpeed;
	public float minRadius;
	public float maxRadius;

	private ConstantForwardMotion cfm;

	void Start() {
		cfm = GetComponent<ConstantForwardMotion> ();
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	void FixedUpdate() {
		Vector3 difference = target.transform.position - transform.position;
		if (difference.magnitude < maxRadius) {
			cfm.speed = maxSpeed;
		} else if (difference.magnitude < minRadius) {
			float ratio = (difference.magnitude - maxRadius) / (minRadius - maxRadius); 
			cfm.speed = (1.0f - ratio) * (maxSpeed) + ratio * (minSpeed);
		} else {
			cfm.speed = minSpeed;
		}
	}
}
