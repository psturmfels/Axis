using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputForwardMotion : MonoBehaviour {
	private Rigidbody rb;
	private InputManager im;
	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody> ();	
		im = GetComponent<InputManager> (); 
	}
	
	void FixedUpdate () {
		float vertAxis = Input.GetAxisRaw("Vertical");
		if (im.GetInputEnabled () && im.GetForwardMotionEnabled() && vertAxis > 0.3f) {
			rb.AddRelativeForce (Vector3.up * speed, ForceMode.Force);
		} else {
			rb.velocity = Vector3.zero;
		}
	}
}
