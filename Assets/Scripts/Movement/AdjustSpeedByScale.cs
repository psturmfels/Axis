using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSpeedByScale : MonoBehaviour {
	private ConstantForwardMotion cfm;
	private Vector3 scaleAxis = Vector3.right;
	private float minScaleAdjust = 450.0f;
	private float maxScaleAdjust = 1200.0f;
	private float minSpeed = 5000.0f;
	private float maxSpeed = 7000.0f;

	void Start () {
		cfm = GetComponent<ConstantForwardMotion> ();
	}

	void FixedUpdate () {
		float currentScale = Vector3.Dot (scaleAxis, transform.localScale);
		if (currentScale >= maxScaleAdjust) {
			cfm.speed = minSpeed;
		} else if (currentScale <= minScaleAdjust) {
			cfm.speed = maxSpeed;
		} else {
			float ratio = (currentScale - minScaleAdjust) / (maxScaleAdjust - minScaleAdjust);
			cfm.speed = minSpeed * ratio + maxSpeed * (1.0f - ratio);
		}
	}
}
