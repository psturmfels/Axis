﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeAlongAxis : MonoBehaviour {
	public int maxShakes = 4;
	public float shakeIncrement = 25.0f;
	public float maxDeviation = 50.0f;
	public Vector3 shakeAxis;

	private Transform rt;
	private Vector3 initialPosition;
	private bool isShaking = false;
	private int numShakesAccumulated = 0;

	// Use this for initialization
	void Start () {
		rt = GetComponent<Transform> ();
		initialPosition = rt.position;
	}
	
	void FixedUpdate () {
		if (isShaking) {
			if (numShakesAccumulated >= maxShakes) {
				rt.position = initialPosition;
				isShaking = false;
				return;
			}
			if (Mathf.Abs (Vector3.Dot (rt.position - initialPosition, shakeAxis)) >= maxDeviation) {
				shakeIncrement = -shakeIncrement;
			}
			rt.position += shakeAxis * shakeIncrement;
			if (Vector3.Distance(rt.position, initialPosition) < shakeIncrement) {
				numShakesAccumulated += 1;
			}
		}
	}

	public void StartShake() {
		rt.position = initialPosition;
		isShaking = true;
		numShakesAccumulated = 0;
	}
}
