using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour {
	private float resolutionWidthRatio = 16.0f;
	private float resolutionHeightRatio = 9.0f;

	void Awake () {
		Resolution res = Screen.currentResolution;
		int currentRatio = res.width / res.height;
		
		if (currentRatio > resolutionWidthRatio / resolutionHeightRatio) {
			float newWidthFloat = res.height * resolutionWidthRatio / resolutionHeightRatio;
			int newWidth = (int)newWidthFloat; 
			Screen.SetResolution (newWidth, res.height, false);
		} else if (currentRatio < resolutionWidthRatio / resolutionHeightRatio) {
			float newHeightFloat = res.width * resolutionHeightRatio / resolutionWidthRatio;
			int newHeight = (int)newHeightFloat;
			Screen.SetResolution (res.width, newHeight, false);
		}
	}
}
