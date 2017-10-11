using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToRed : MonoBehaviour {
	public float InterpolationRate;

	private MeshRenderer mr;
	private bool isFading = false;

	void Start() {
		mr = GetComponent<MeshRenderer> ();
		mr.material = new Material (mr.material);
	}

	public void StartFade () {
		isFading = true;
	}

	void FixedUpdate() {
		if (isFading) {
			if (mr.material.color == Color.red) {
				isFading = false;
			} else {
				mr.material.color = Color.Lerp (mr.material.color, Color.red, InterpolationRate);
			}
		}
	}

}
