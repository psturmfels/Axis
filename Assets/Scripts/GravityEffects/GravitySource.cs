using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour {
	private float gravity = 9.8f;
	private Rigidbody rb;
	public Vector3 initialVelocity;
	public int ID;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = initialVelocity;
		Time.timeScale = 5.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("GravityItem");
		foreach (GameObject obj in targets) {
			if (obj.GetInstanceID() != gameObject.GetInstanceID()) {
				Rigidbody otherRB = obj.GetComponent<Rigidbody> ();
				Transform tr = obj.GetComponent<Transform> ();
				float gravityForce = -1.0f * gravity * otherRB.mass * rb.mass / Mathf.Pow (Vector3.Distance (tr.position, transform.position), 2);
				otherRB.AddExplosionForce (gravityForce, transform.position, float.MaxValue, 0.0f, ForceMode.Force);
			}
		}
	}
}
