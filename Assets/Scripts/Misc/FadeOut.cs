using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
	private MeshRenderer mr;
	public float alphaFadeRate;
	public bool multiColored = true;
	public bool shouldDestroy = true;
	public bool startAlphaZero = false;

	private bool shouldFade = true;
	private float waitToFade = 1.0f;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
		mr.material = new Material (mr.material);
		if (multiColored) {
			Vector3 ColorOffset = Random.insideUnitSphere;
			mr.material.color = mr.material.color + new Color (ColorOffset.x, ColorOffset.y, ColorOffset.z);
		}
		if (startAlphaZero) {
			mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.0f);
			shouldFade = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (mr.material.color.a < alphaFadeRate) {
			shouldFade = false;
			if (shouldDestroy) {
				Destroy (gameObject);
			} else {
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.0f);
			}
		} else if (shouldFade) {
			mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, mr.material.color.a - alphaFadeRate);
		}
	}

	public void SetAlphaToOne() {
		shouldFade = false;
		mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, 1.0f);
		Invoke ("StartFade", waitToFade);
	}

	void StartFade() {
		shouldFade = true;
	}
}
