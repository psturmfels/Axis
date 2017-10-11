using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFader : MonoBehaviour {
	private Text tutorialText;
	private Image imagePanel;
	private float alphaFadeRate = 0.02f;
	private bool isChangingText = false;
	private bool isFadingOut = false;
	private bool isFadingAll = false;
	private bool isFadingIn = false;
	private string nextString;

	void Start () {
		tutorialText = GetComponentInChildren <Text> ();
		imagePanel = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isChangingText) {
			if (isFadingOut) {
				if (tutorialText.color.a - alphaFadeRate < 0.0f) {
					tutorialText.text = nextString;
					isFadingOut = false;
				} else {
					tutorialText.color = new Color (tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, Mathf.Max (tutorialText.color.a - alphaFadeRate, 0.0f));
				}
			} else {
				if (tutorialText.color.a + alphaFadeRate > 1.0f) {
					isChangingText = false;
				} else {
					tutorialText.color = new Color (tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, Mathf.Min (tutorialText.color.a + alphaFadeRate, 1.0f));
				}
			}
		} else if (isFadingAll) {
			if (tutorialText.color.a - alphaFadeRate < 0.0f && imagePanel.color.a - alphaFadeRate < 0.0f) {
				isFadingAll = false;
			} else {
				tutorialText.color = new Color (tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, Mathf.Max (tutorialText.color.a - alphaFadeRate, 0.0f));
				imagePanel.color = new Color (imagePanel.color.r, imagePanel.color.g, imagePanel.color.b, Mathf.Max (imagePanel.color.a - alphaFadeRate * 0.5f, 0.0f));
			}
		} else if (isFadingIn) {
			if (tutorialText.color.a + alphaFadeRate > 1.0f && imagePanel.color.a + alphaFadeRate > 0.4f) {
				isFadingIn = false;
			} else {
				tutorialText.color = new Color (tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, Mathf.Min (tutorialText.color.a + alphaFadeRate, 1.0f));
				imagePanel.color = new Color (imagePanel.color.r, imagePanel.color.g, imagePanel.color.b, Mathf.Min (imagePanel.color.a + alphaFadeRate * 0.3f, 0.4f));
			}
		}
	}

	public void StartChangeText(string newText) {
		nextString = newText;
		isFadingOut = true;
		isChangingText = true;
	}

	public void StartFadeOut() {
		isFadingAll = true; 
	}

	public void StartFadeIn() {
		isFadingIn = true;
	}
}
