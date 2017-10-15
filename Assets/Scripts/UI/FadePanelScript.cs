using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanelScript : MonoBehaviour {
	private float alphaFadeRate = 0.05f;
	private Image panelImage;

	private bool shouldFadeOut = false;
	private bool shouldFadeIn = false;

	void Start () {
		panelImage = GetComponent<Image> ();
		StartFadeOut ();
	}

	public void StartFadeIn() {
		shouldFadeIn = true;
	}

	public void StartFadeOut() {
		shouldFadeOut = true;
	}
	
	void FixedUpdate () {
		if (shouldFadeOut) {
			if (panelImage.color.a < alphaFadeRate) {
				shouldFadeOut = false;
				gameObject.SetActive (false);
				panelImage.color = new Color (panelImage.color.r, panelImage.color.g, panelImage.color.b, 0.0f);
			} else {
				panelImage.color = new Color (panelImage.color.r, panelImage.color.g, panelImage.color.b, panelImage.color.a - alphaFadeRate);
			}
		} else if (shouldFadeIn) {
			if (panelImage.color.a + alphaFadeRate > 1.0f) {
				shouldFadeIn = false;
				panelImage.color = new Color (panelImage.color.r, panelImage.color.g, panelImage.color.b, 1.0f);
			} else {
				panelImage.color = new Color (panelImage.color.r, panelImage.color.g, panelImage.color.b, panelImage.color.a + alphaFadeRate);
			}
		}
	}
}
