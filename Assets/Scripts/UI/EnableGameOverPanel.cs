using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableGameOverPanel : MonoBehaviour {
	public GameObject gameOverPanel;
	public Text gameOverText;
	public ComboTrackerScript cts;

	public void StartEnablePanel() {
		Invoke ("EnablePanel", 4.0f);
	}

	void EnablePanel() {
		gameOverText.text = "Game Over\nYour Score:\n" + cts.GetCurrentScore ();
		gameOverPanel.SetActive(true);
	}
}
