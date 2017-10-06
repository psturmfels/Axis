using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousGrow : MonoBehaviour {
	public Vector3 growDirections;
	public Vector3 growSpeeds;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localScale += growSpeeds;
	}
}
