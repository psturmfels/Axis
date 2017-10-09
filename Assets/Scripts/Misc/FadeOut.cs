using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
	private MeshRenderer mr;
	public float alphaFadeRate;
	public bool multiColored = true;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
		if (multiColored) {
			mr.material = new Material (mr.material);
			Vector3 ColorOffset = Random.insideUnitSphere;
			mr.material.color = mr.material.color + new Color (ColorOffset.x, ColorOffset.y, ColorOffset.z);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (mr.material.color.a < alphaFadeRate) {
			Destroy (gameObject);
		} else {
			mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, mr.material.color.a - alphaFadeRate);
		}
	}
}
