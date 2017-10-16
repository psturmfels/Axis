using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTrackerScript : MonoBehaviour {
	private float comboTime = 0.5f;
	private int currentComboMeter = 0;
	private float accumulatedComboTime;
	private int currentScore = 0;
	private int runningComboScore = 0;
	private bool isComboing = false;
	private Vector3 addedScorePosition = new Vector3 (3500.0f, 2000.0f, -450.0f);

	public Text ScoreCountText;

	public int GetCurrentScore() {
		return currentScore;
	}

	void FixedUpdate () {
		if (isComboing) {
			accumulatedComboTime += Time.deltaTime;
			if (accumulatedComboTime > comboTime) {
				FinishCombo ();
			}
		}
	}

	void FinishCombo() {
		if (currentComboMeter > 1) {
			displayComboMultiplierAtPlayer ();
		}
		displayAddedScore ();
		Application.ExternalCall("kongregate.stats.submit", "BestCombo", currentComboMeter);

		currentScore += currentComboMeter * runningComboScore;
		ScoreCountText.text = currentScore.ToString ();
		isComboing = false;
		accumulatedComboTime = 0.0f;
		runningComboScore = 0;
		currentComboMeter = 0;
	}

	public void NewEnemyKilled(int deathScore) {
		Application.ExternalCall("kongregate.stats.submit", "EnemiesKilled", 1);
		accumulatedComboTime = 0.0f;
		isComboing = true;
		currentComboMeter += 1;
		runningComboScore += deathScore;
	}

	void displayComboMultiplierAtPlayer() {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			Vector3 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;

			GameObject fadeTextPrefab = Resources.Load ("FadeText") as GameObject;
			Vector3 textPosition = new Vector3 (playerPosition.x + 100.0f, playerPosition.y + 100.0f, -450.0f);
			GameObject WorldCanvas = GameObject.Find ("WorldCanvas");
			GameObject fadeText = Instantiate (fadeTextPrefab, textPosition, Quaternion.identity, WorldCanvas.transform);
			fadeText.GetComponent<Text> ().text = "x" + currentComboMeter;
		}
	}

	void displayAddedScore() {
		GameObject fadeTextPrefab = Resources.Load ("FadeText") as GameObject;
		GameObject WorldCanvas = GameObject.Find ("WorldCanvas");
		GameObject fadeText = Instantiate (fadeTextPrefab, addedScorePosition, Quaternion.identity, WorldCanvas.transform);
		fadeText.GetComponent<Text> ().text = "+" + currentComboMeter * runningComboScore;
	}
}
