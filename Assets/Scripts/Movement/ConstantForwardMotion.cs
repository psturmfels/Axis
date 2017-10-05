using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantForwardMotion : MonoBehaviour {
	private Rigidbody rb;
	public float speed = 15000.0f;
	public Vector3 forwardDirection;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		rb.AddRelativeForce (forwardDirection * speed, ForceMode.Force);
	}
}
