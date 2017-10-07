using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	private GameObject[] spawnedEnemies;
	public int currentNumEnemies;
	private Queue<int> availableIndices;
	private float minSpawnWait = 1.0f;
	private float maxSpawnWait = 10.0f;
	private float spawnXMax = 5000.0f;
	private float spawnXMin = 4500.0f;
	private float spawnYMax = 3000.0f;
	private float spawnYMin = 2500.0f;

	public int maxEnemies;
	public GameObject[] enemyPrefabs;
	public float[] enemyProbabilities;
	

	void Start () {
		spawnedEnemies = new GameObject[maxEnemies];
		availableIndices = new Queue<int> ();
		for (int i = 0; i < maxEnemies; ++i) {
			availableIndices.Enqueue (i);
		}
		currentNumEnemies = 0;
		SpawnNextEnemy ();
	}

	void SpawnNextEnemy() {
		if (currentNumEnemies >= maxEnemies) {
			Invoke ("SpawnNextEnemy", Random.Range (minSpawnWait, maxSpawnWait));
			return;
		}

		float newXPos = Random.Range (spawnXMin, spawnXMax);
		float newYPos = Random.Range (spawnYMin, spawnYMax);
		if (Random.Range (0, 2) == 0) {
			newXPos = -newXPos;
		}
		if (Random.Range (0, 2) == 0) {
			newYPos = -newYPos;
		}
		Vector3 newEnemyPosition = new Vector3(newXPos, newYPos, 0.0f);

		GameObject nextEnemy = null;
		float enemyChoice = Random.Range (0.0f, 1.0f);
		for (int i = 0; i < maxEnemies; ++i) {
			if (enemyChoice < enemyProbabilities [i]) {
				nextEnemy = Instantiate(enemyPrefabs[i], newEnemyPosition, Quaternion.identity) as GameObject;
				break;
			}
		}
		if (nextEnemy == null) {
			Invoke ("SpawnNextEnemy", Random.Range (minSpawnWait, maxSpawnWait));
			return;
		}

		currentNumEnemies += 1;
		int nextIndex = availableIndices.Dequeue ();
		spawnedEnemies [nextIndex] = nextEnemy;
		nextEnemy.GetComponent<EnemyDie> ().SetIndex (nextIndex);
		Invoke ("SpawnNextEnemy", Random.Range (minSpawnWait, maxSpawnWait));
	}

	public void RegisterDeathAtIndex(int deathIndex) {
		spawnedEnemies [deathIndex] = null;
		currentNumEnemies -= 1;
		availableIndices.Enqueue (deathIndex);
	}
}
