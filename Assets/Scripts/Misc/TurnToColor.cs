using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToColor : MonoBehaviour {
	private Material mat;
	private Color original_color;

	// Use this for initialization
	void Awake () {
		mat = GetComponent<MeshRenderer> ().material;
		original_color = mat.color;
	}
	
	void FixedUpdate () {
		ProcessColor ();
	}

	void ProcessColor() {
		mat.color = Color.Lerp(mat.color, original_color, 0.1f);
	}

	public void ChangeColor(Color newColor) {
		mat.color = newColor;
	}

	public void ChangeOriginalColor(Color newColor) {
		original_color = newColor;
	}

	public Color GetOriginalColor() {
		return original_color;
	}
}
