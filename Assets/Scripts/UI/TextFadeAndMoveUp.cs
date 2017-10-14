using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeAndMoveUp : MonoBehaviour {
	private Text textObject;
	private float alphaFadeRate = 0.015f;
	private float moveUpIncrement = 5.0f;

	void Start () {
		textObject = GetComponent<Text> ();
	}

	void FixedUpdate () {
		if (textObject.color.a < alphaFadeRate) {
			Destroy (gameObject);
		} else {
			textObject.color = new Color (textObject.color.r, textObject.color.g, textObject.color.b, textObject.color.a - alphaFadeRate);
		}

		transform.position += Vector3.up * moveUpIncrement;
	}
}
