using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObserver : MonoBehaviour {
	public GameObject redCubePrefab;
	private float spawnXMax = 5000.0f;
	private float spawnXMin = 4500.0f;
	private float spawnYMax = 3000.0f;
	private float spawnYMin = 2500.0f;
	private int startSpawnIndex = 6;
	private TutorialFader tf;
	private bool initiatedFlagCall = false;
	private enum ProgressType {
		WaitTime,
		KeyInput,
		FlagCall
	}
	private string[] tutorialMessages = new string[] {
		"Arrow keys to move",
		"The red cube is not your friend",
		"The space bar is your friend",
		"Press it a second time to attack",

		"'W' key activates warp drive",
		"Press 'E' to pause at any time",

		"In the vastness of space...",
		"...everything is an enemy...",
		"...except this tutorial panel...",
		@"...I'm here to help ¯\_(ツ)_/¯"
	};
	private ProgressType[] tutorialProgressTypes = new ProgressType[] {
		ProgressType.KeyInput,
		ProgressType.WaitTime,
		ProgressType.WaitTime,
		ProgressType.FlagCall,

		ProgressType.KeyInput,
		ProgressType.WaitTime,

		ProgressType.WaitTime,
		ProgressType.WaitTime,
		ProgressType.WaitTime,
		ProgressType.WaitTime
	};
	private KeyCode[] keyInputs = new KeyCode[] {
		KeyCode.UpArrow,
		KeyCode.None,
		KeyCode.None,
		KeyCode.None,

		KeyCode.W,
		KeyCode.None,

		KeyCode.None,
		KeyCode.None,
		KeyCode.None,
		KeyCode.None
	};
	private float[] waitTimes = new float[] { 0.0f, 6.0f, 4.0f, 0.0f, 0.0f, 4.0f, 4.0f, 4.0f, 4.0f, 4.0f, 4.0f };

	private int currentTutorialIndex = 0;
	private float waitToGetInputTime = 2.0f;
	private bool isWaitingForKeyInput = false;
	private int spawnCubeIndex = 1;
	private bool isDoingTutorial = true;

	public bool GetIsDoingTutorial() {
		return isDoingTutorial;
	}

	void Start () {
		tf = GetComponent<TutorialFader> ();
		tf.StartFadeIn ();
		Invoke ("EnableWaitingForKeyInput", waitToGetInputTime);
	}

	void Update () {
		if (isWaitingForKeyInput && Input.GetKeyDown (keyInputs [currentTutorialIndex])) {
			initiatedFlagCall = false;
			ProgressToNextMessage ();
		}
	}

	public void InitiateFlagCall(int calledIndex) {
		if (currentTutorialIndex <= calledIndex) {
			currentTutorialIndex = calledIndex;
			initiatedFlagCall = false;
			ProgressToNextMessage ();
			initiatedFlagCall = true;
		}
	}

	void ProgressToNextMessage() {
		if (initiatedFlagCall) {
			return;
		}

		isWaitingForKeyInput = false;
		if (currentTutorialIndex == tutorialMessages.Length - 1) {
			isDoingTutorial = false;
			tf.StartFadeOut ();
		} else {
			currentTutorialIndex += 1;
			if (currentTutorialIndex == startSpawnIndex) {
				GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ().enabled = true;
			}

			if (currentTutorialIndex == spawnCubeIndex) {
				SpawnTutorialCube ();
			}

			tf.StartChangeText (tutorialMessages [currentTutorialIndex]);
			if (tutorialProgressTypes [currentTutorialIndex] == ProgressType.WaitTime) {
				Invoke ("ProgressToNextMessage", waitTimes [currentTutorialIndex]);
			} else if (tutorialProgressTypes [currentTutorialIndex] == ProgressType.KeyInput) {
				Invoke ("EnableWaitingForKeyInput", waitToGetInputTime);
			}
		}
	}

	void EnableWaitingForKeyInput() {
		isWaitingForKeyInput = true;
	}

	void SpawnTutorialCube() {
		float newXPos = Random.Range (spawnXMin, spawnXMax);
		float newYPos = Random.Range (spawnYMin, spawnYMax);
		if (Random.Range (0, 2) == 0) {
			newXPos = -newXPos;
		}
		if (Random.Range (0, 2) == 0) {
			newYPos = -newYPos;
		}
		Vector3 newEnemyPosition = new Vector3(newXPos, newYPos, 0.0f);

		GameObject nextEnemy = Instantiate(redCubePrefab, newEnemyPosition, Quaternion.identity) as GameObject;
		nextEnemy.AddComponent<RedCubeFlagCall> ();
	}
}
