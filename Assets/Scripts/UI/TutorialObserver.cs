using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObserver : MonoBehaviour {
	public GameObject redCubePrefab;
	private float spawnXMax = 5000.0f;
	private float spawnXMin = 4500.0f;
	private float spawnYMax = 3000.0f;
	private float spawnYMin = 2500.0f;
	private TutorialFader tf;
	private enum ProgressType {
		WaitTime,
		KeyInput,
		FlagCall
	}
	private string[] tutorialMessages = new string[] {
		"I wake up, alone in the vastness of space. But I do notice that I can use the left arrow key to turn counter clockwise.",
		"Unsurprisingly, the right arrow key seems to allow me to turn clockwise.",
		"How odd. I wonder to myself if I can move forward as well. Perhaps the up arrow key would give me the desired result.", 
		"I also notice that I can flip around with the down arrow key, but I can't yet deduce why that would be better than turning.",

		"Am I trapped here, in this region of space? I endeavor to find out by attempting to leave the screen boundary.",
		"My efforts to escape are futile.",
		"Why was I put here? What does it mean? Am I alone?",

		"The moment I have that thought, a strange red cube appears. Is it here to help me? I decide to approach it.",
		"Ouch! As I run away from the ominous platonic solid, I realize that running isn't a sustainable strategy...", 
		"I recall that I have a warp attack. I decide to use the spacebar to send out a projection at my enemy.",

		"Nothing seem to have happened. This time, I decide to press the spacebar a second time when the projection is near my enemy...",
		"After shattering the red cube, I note that I can only send out a projection when the light blue bar in the bottom left is full.",
		"Curiously, I recall that I also have a warp drive I can use by pressing 'W'.",

		"I have a feeling any other enemies would be destroyed if I sped through them while using my warp drive.",
		"But it seems I am out of warp drive fuel. I am confident I will figure out how to get more as time goes on.",
		"Now that I have all the information I need, I suddenly feel surrounded by enemies..."
	};
	private ProgressType[] tutorialProgressTypes = new ProgressType[] {
		ProgressType.KeyInput,
		ProgressType.KeyInput,
		ProgressType.KeyInput,
		ProgressType.KeyInput,

		ProgressType.FlagCall,
		ProgressType.WaitTime,
		ProgressType.WaitTime,

		ProgressType.FlagCall,
		ProgressType.WaitTime,
		ProgressType.KeyInput,

		ProgressType.FlagCall,
		ProgressType.WaitTime,
		ProgressType.KeyInput,

		ProgressType.WaitTime,
		ProgressType.WaitTime,
		ProgressType.WaitTime
	};
	private KeyCode[] keyInputs = new KeyCode[] {
		KeyCode.LeftArrow,
		KeyCode.RightArrow,
		KeyCode.UpArrow,
		KeyCode.DownArrow,

		KeyCode.None,
		KeyCode.None,
		KeyCode.None,

		KeyCode.None,
		KeyCode.None,
		KeyCode.Space,

		KeyCode.None,
		KeyCode.None,
		KeyCode.W,

		KeyCode.None,
		KeyCode.None,
		KeyCode.None
	};
	private int currentTutorialIndex = 0;
	private float defaultWaitTime = 6.0f;
	private float waitToGetInputTime = 2.0f;
	private float waitToGetFlagTime = 0.5f;
	private bool isWaitingForKeyInput = false;
	private bool isWaitingForFlagCall = false;
	private int spawnCubeIndex = 7;
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
			ProgressToNextMessage ();
		}
	}

	public void InitiateFlagCall(int calledIndex) {
		if (isWaitingForFlagCall && calledIndex == currentTutorialIndex) {
			ProgressToNextMessage ();
		}
	}

 	void ProgressToNextMessage() {
		isWaitingForKeyInput = false;
		isWaitingForFlagCall = false;
		if (currentTutorialIndex == tutorialMessages.Length - 1) {
			isDoingTutorial = false;
			tf.StartFadeOut ();
			GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ().enabled = true;
		} else {
			currentTutorialIndex += 1;
			if (currentTutorialIndex == spawnCubeIndex) {
				SpawnTutorialCube ();
			}

			tf.StartChangeText (tutorialMessages [currentTutorialIndex]);
			if (tutorialProgressTypes [currentTutorialIndex] == ProgressType.WaitTime) {
				Invoke ("ProgressToNextMessage", defaultWaitTime);
			} else if (tutorialProgressTypes [currentTutorialIndex] == ProgressType.KeyInput) {
				Invoke ("EnableWaitingForKeyInput", waitToGetInputTime);
			} else if (tutorialProgressTypes [currentTutorialIndex] == ProgressType.FlagCall) {
				Invoke ("EnableWaitingForFlagCall", waitToGetFlagTime);
			}
		}
	}

	void EnableWaitingForFlagCall() {
		isWaitingForFlagCall = true;
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
