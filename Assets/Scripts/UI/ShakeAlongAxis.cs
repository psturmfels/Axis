using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeAlongAxis : MonoBehaviour {
	public int maxShakes = 6;
	public float shakeIncrement = 1.0f;
	public float maxDeviation = 4.0f;
	public Vector3 shakeAxis;

	private RectTransform rt;
	private Vector3 initialPosition;
	private bool isShaking = false;
	private int numShakesAccumulated = 0;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
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
			if (rt.position == initialPosition) {
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
