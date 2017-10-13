using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCubeFlagCall : MonoBehaviour {
	private int dieIndex = 3;

	void Start() {
		GetComponent<EnemyDie> ().SetIndex (100);
	}

	public void Die() {
		if (GameObject.Find("TutorialPanel") != null) {
			TutorialObserver to = GameObject.Find("TutorialPanel").GetComponent<TutorialObserver> ();
			to.InitiateFlagCall (dieIndex);
		}
	}
}
