using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputForwardMotion : MonoBehaviour {
	private Rigidbody rb;
	private InputManager im;
	private bool didInputLastFrame = false;
	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody> ();	
		im = GetComponent<InputManager> (); 
	}
	
	void FixedUpdate () {
		float vertAxis = Input.GetAxisRaw("Vertical");
		if (im.GetInputEnabled () && im.GetForwardMotionEnabled() && vertAxis > 0.3f) {
			rb.AddRelativeForce (Vector3.up * speed, ForceMode.Force);
			didInputLastFrame = true;
		} else if (didInputLastFrame) {
			rb.velocity = Vector3.zero;
			didInputLastFrame = false;
		}
	}
}
