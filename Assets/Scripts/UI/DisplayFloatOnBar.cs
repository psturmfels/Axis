using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFloatOnBar : MonoBehaviour {
	public Scrollbar[] dispBar;
	private int warpIndex = 2;
		
	public void SetDispValue(float newDispValue, int index) {
		dispBar[index].size = newDispValue;
		if (index == warpIndex) {
			Color oldColor = dispBar [index].colors.disabledColor;
			if (newDispValue == 1.0f) {
				Color opaque = new Color (oldColor.r, oldColor.g, oldColor.b, 0.4f);
				ColorBlock cb = dispBar [index].colors;
				cb.disabledColor = opaque;
				dispBar [index].colors = cb;
			} else {
				Color opaque = new Color (oldColor.r, oldColor.g, oldColor.b, 0.2f);
				ColorBlock cb = dispBar [index].colors;
				cb.disabledColor = opaque;
				dispBar [index].colors = cb;
			}
		}
	}

	public void ErrorAtIndex(int index) {
		dispBar [index].GetComponent<ShakeAlongAxis> ().StartShake ();
	}
}
