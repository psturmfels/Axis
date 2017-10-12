using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateTargetEnemyShip : MonoBehaviour {
	private ConstantFaceTransform cft;
	private bool isChasingPlayer = true;
	private float playerFocusTimeMin = 5.0f;
	private float playerFocusTimeMax = 16.0f;
	private float enemyFocusTimeMin = 1.0f;
	private float enemyFocusTimeMax = 5.0f;

	void Start () {
		cft = GetComponent<ConstantFaceTransform> ();
		Invoke ("SwitchTargets", Random.Range (playerFocusTimeMin, playerFocusTimeMax));
	}

	void FixedUpdate () {
		if (cft.target == null) {
			if (GameObject.FindGameObjectWithTag ("Player") != null) {
				cft.target = GameObject.FindGameObjectWithTag ("Player").transform;
				isChasingPlayer = true;
			}
		}
	}

	void SwitchTargets() {
		if (isChasingPlayer) {
			isChasingPlayer = false; 
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			int randomIndex = Random.Range (0, enemies.Length);
			if (enemies [randomIndex] != null && enemies [randomIndex].layer == LayerMask.NameToLayer ("Enemy")) {
				cft.target = enemies [randomIndex].transform;
			}
			Invoke ("SwitchTargets", Random.Range (enemyFocusTimeMin, enemyFocusTimeMax));
		} else {
			isChasingPlayer = true;
			if (GameObject.FindGameObjectWithTag ("Player") != null) {
				cft.target = GameObject.FindGameObjectWithTag ("Player").transform;
			}
			Invoke ("SwitchTargets", Random.Range (playerFocusTimeMin, playerFocusTimeMax));
		}
	}
}
