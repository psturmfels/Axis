using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	private GameObject[] spawnedEnemies;
	public int currentNumEnemies;
	private Queue<int> availableIndices;
	private float spawnXMax = 5000.0f;
	private float spawnYMax = 3000.0f;
	private int numSpheres = 0;
	private int sphereIndex = 3;
	private int currentNumEvolves = 0;
	private float enemyTimeMultiplier = 1.1f;
	private float timeMultiplierIncrement = 0.05f;
	private ComboTrackerScript cts;

	public int maxEnemies;
	public int maxNumEvolves;
	public float timeBetweenEvolves;
	public GameObject[] enemyPrefabs;
	public float[] Probabilities;
	public float[] EvolveRate;
	public KeyCode hardcoreKeycode;
	private bool hardcoreModeActive = false;

	void Start () {
		cts = GetComponent<ComboTrackerScript> ();
		spawnedEnemies = new GameObject[maxEnemies];
		availableIndices = new Queue<int> ();
		for (int i = 0; i < maxEnemies; ++i) {
			availableIndices.Enqueue (i);
		}
		currentNumEnemies = 0;
		SpawnNextEnemy ();
		Invoke ("EvolveProbabilities", timeBetweenEvolves);
	}


	void Update() {
		if (Input.GetKeyDown (hardcoreKeycode)) {
			hardcoreModeActive = !hardcoreModeActive;
		}
	}


	private float getNextSpawnTime() {
		return 0.3f - enemyTimeMultiplier * Mathf.Log (1.0f - Random.Range (0.0f, 0.98f));
	}

	void SpawnNextEnemy() {
		if (currentNumEnemies >= maxEnemies) {
			Invoke ("SpawnNextEnemy", getNextSpawnTime ());
			return;
		}

		float newXPos = 0.0f;
		float newYPos = 0.0f; 
		if (Random.Range (0, 2) == 0) {
			newXPos = Random.Range (-spawnXMax, spawnXMax);
			newYPos = spawnYMax;
			if (Random.Range (0, 2) == 0) {
				newYPos = -newYPos;
			}
		} else {
			newXPos = spawnXMax;
			newYPos = Random.Range (-spawnYMax, spawnYMax);
			if (Random.Range (0, 2) == 0) {
				newXPos = -newXPos;
			}
		}

		if (Random.Range (0, 2) == 0) {
			newXPos = -newXPos;
		}

		Vector3 newEnemyPosition = new Vector3(newXPos, newYPos, 0.0f);

		GameObject nextEnemy = null;
		if (hardcoreModeActive) {
			int randomIndexChoice = Random.Range (0, enemyPrefabs.Length);
			if (randomIndexChoice == sphereIndex && numSpheres > 0) {
				nextEnemy = Instantiate (enemyPrefabs [0], newEnemyPosition, Quaternion.identity) as GameObject;
			} else {
				nextEnemy = Instantiate (enemyPrefabs [randomIndexChoice], newEnemyPosition, Quaternion.identity) as GameObject;
			}
		} else {
			float enemyChoice = Random.Range (0.0f, 1.0f);
			for (int i = 0; i < Probabilities.Length; ++i) {
				float enemyProbability = Probabilities [i];
				
				if (enemyChoice < enemyProbability) {
					if (i == sphereIndex && numSpheres > 0) {
						nextEnemy = Instantiate (enemyPrefabs [0], newEnemyPosition, Quaternion.identity) as GameObject;
						break;
					} else {
						nextEnemy = Instantiate (enemyPrefabs [i], newEnemyPosition, Quaternion.identity) as GameObject;
						break;
					}
				}
			}
		}

		if (nextEnemy == null) {
			Invoke ("SpawnNextEnemy", getNextSpawnTime ());
			return;
		}

		currentNumEnemies += 1;
		int nextIndex = availableIndices.Dequeue ();
		spawnedEnemies [nextIndex] = nextEnemy;
		if (nextEnemy.GetComponent<EnemyDie> () != null) {
			nextEnemy.GetComponent<EnemyDie> ().SetIndex (nextIndex);
		} else if (nextEnemy.GetComponent<SphereEnemyContact> () != null) {
			nextEnemy.GetComponent<SphereEnemyContact> ().SetIndex (nextIndex);
			numSpheres += 1;
		}
		Invoke ("SpawnNextEnemy", getNextSpawnTime ());
	}

	public void reduceNumSpheres() {
		numSpheres = Mathf.Max (0, numSpheres - 1);
	}

	public void RegisterDeathAtIndex(int deathIndex, int deathScore, bool killedByPlayer = false) {
		if (!enabled) {
			return; 
		}
		if (killedByPlayer) {
			cts.NewEnemyKilled (deathScore);
		}
		spawnedEnemies [deathIndex] = null;
		currentNumEnemies -= 1;
		availableIndices.Enqueue (deathIndex);
	}

	void EvolveProbabilities() {
		if (currentNumEvolves >= maxNumEvolves) {
			return;
		}

		enemyTimeMultiplier -= timeMultiplierIncrement;
		currentNumEvolves += 1;
		for (int i = 0; i < Probabilities.Length; ++i) {
			if (Probabilities [i] == 1.0f) {
				Probabilities [i] += EvolveRate [i];
				break;
			}
			Probabilities [i] += EvolveRate [i];
		}
			
		if (currentNumEvolves < maxNumEvolves) {
			Invoke ("EvolveProbabilities", timeBetweenEvolves);
		}
	}
}
