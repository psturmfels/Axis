using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFloatOnBar : MonoBehaviour {
	public Transform[] dispBar;
	private int warpIndex = 2;
		
	public void SetDispValue(float newDispValue, int index) {
		Vector3 currentScale = dispBar [index].localScale;
		Vector3 currentPosition = dispBar [index].localPosition;
		dispBar [index].localScale = new Vector3 (newDispValue, currentScale.y, currentScale.z);
		dispBar [index].localPosition = new Vector3 (-0.5f + newDispValue * 0.5f, currentPosition.y, currentPosition.z);
		if (index == warpIndex) {
			Color oldColor = dispBar [index].gameObject.GetComponent<MeshRenderer> ().material.color;
			if (newDispValue == 1.0f) {
				Color opaque = new Color (oldColor.r, oldColor.g, oldColor.b, 0.9f);
				dispBar [index].gameObject.GetComponent<MeshRenderer> ().material.color = opaque;
			} else {
				Color clear = new Color (oldColor.r, oldColor.g, oldColor.b, 0.5f);
				dispBar [index].gameObject.GetComponent<MeshRenderer> ().material.color = clear;
			}
		}
	}

	public void ErrorAtIndex(int index) {
		dispBar [index].GetComponentInParent<ShakeAlongAxis> ().StartShake ();
	}
}
