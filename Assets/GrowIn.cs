using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowIn : MonoBehaviour {
	private Vector3 startScale = new Vector3 (1600.0f, 1600.0f, 1.0f);
	private Vector3 endScale = new Vector3 (400.0f, 400.0f, 1.0f);
	private Vector3 scaleIncrement = new Vector3 (50.0f, 50.0f, 0.0f);

	private float alphaFadeRate = 0.05f;

	private MeshRenderer mr;

	private bool shouldGrowIn = true;
	private bool shouldFadeIn = true;
	private float waitTime = 0.5f;

	void Start () {
		mr = GetComponent<MeshRenderer> ();
		mr.material = new Material (mr.material);
		mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.0f);
		transform.localScale = startScale;
	}
	
	void FixedUpdate () {
		if (shouldGrowIn) {
			if (transform.localScale.x - scaleIncrement.x <= endScale.x) {
				transform.localScale = endScale;
				shouldGrowIn = false;

				Invoke ("AddFadeOut", waitTime);
			} else {
				transform.localScale -= scaleIncrement;
			}
		}

		if (shouldFadeIn) {
			if (mr.material.color.a + alphaFadeRate > 1.0f) { 
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, 1.0f);
				shouldFadeIn = false;
			} else {
				mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, mr.material.color.a + alphaFadeRate);
			}
		}
	}

	void AddFadeOut() {
		FadeOut fo = gameObject.AddComponent<FadeOut> ();
		fo.multiColored = false;
		fo.alphaFadeRate = 0.03f;
	}
}
