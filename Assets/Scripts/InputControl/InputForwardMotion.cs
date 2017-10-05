using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputForwardMotion : MonoBehaviour {
	private Rigidbody rb;
	private InputManager im;
	public float speed;
	private bool shouldInputForce = false;

	void Start () {
		rb = GetComponent<Rigidbody> ();	
		im = GetComponent<InputManager> (); 
	}
	
	void FixedUpdate () {
		float vertAxis = Input.GetAxis("Vertical");
		if (im.GetInputEnabled () && vertAxis > 0.1f) {
			rb.AddRelativeForce (Vector3.up * speed, ForceMode.Force);
		} 
	}
}
