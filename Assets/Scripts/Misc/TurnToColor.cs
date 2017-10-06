using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToColor : MonoBehaviour {
	private Material mat;
	private Color original_color;
	private bool isReturning = false;


	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer> ().material;
		original_color = mat.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReturning) {
			ProcessColor ();
		}
	}

	void ProcessColor() {
		if (mat.color == original_color) {
			isReturning = false;
		}
		mat.color = Color.Lerp(mat.color, original_color, 0.1f);
	}

	public void ChangeColor(Color newColor) {
		mat.color = newColor;
	}

	public void ReturnToOriginalColor() {
		isReturning = true;
	}
}
