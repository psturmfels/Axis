using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpeedUpOnEnter : MonoBehaviour {
	public float intermediateSpeed;
	public float maxSpeed;
	public float intermediateWaitTime; 
	public float speedIncrementIntermediate;
	public float speedIncrementMax;
	public string containerTag;

	private ConstantForwardMotion cfm;
	private ConstantFaceTransform cft;
	private FadeToRed ftr;
	private bool isSpeedingUP = false;
	private bool reachedIntermediate = false;
	private bool isWaitingAtIntermediate = false;

	// Use this for initialization
	void Start () {
		ftr = GetComponent<FadeToRed> ();
		cfm = GetComponent<ConstantForwardMotion> ();
		cft = GetComponent<ConstantFaceTransform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isSpeedingUP) {
			if (reachedIntermediate && !isWaitingAtIntermediate) {
				if (Mathf.Abs (maxSpeed - cfm.speed) < speedIncrementMax) {
					cfm.speed = maxSpeed;
					isSpeedingUP = false; 
				} else {
					cfm.speed += speedIncrementMax * Mathf.Sign (maxSpeed - cfm.speed);
				}
			} else {
				if (Mathf.Abs (intermediateSpeed - cfm.speed) < speedIncrementIntermediate) {
					isWaitingAtIntermediate = true;
					reachedIntermediate = true;
					cfm.speed = intermediateSpeed;
					Invoke ("StartSpeedToMax", intermediateWaitTime);
					Invoke ("StartFadeToRed", intermediateWaitTime * 0.85f);
				} else {
					cfm.speed += speedIncrementIntermediate * Mathf.Sign (intermediateSpeed - cfm.speed);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!isSpeedingUP && other.gameObject.CompareTag (containerTag)) {
			StartSpeedUp ();
		}
	}

	void StartFadeToRed() {
		ftr.StartFade ();
	}

	void StartSpeedToMax() {
		isWaitingAtIntermediate = false; 
		if (cft != null) {
			cft.enabled = false; 
		}
	}

	void StartSpeedUp() {
		isSpeedingUP = true;
	}
}
