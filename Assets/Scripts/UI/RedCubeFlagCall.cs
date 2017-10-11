using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCubeFlagCall : MonoBehaviour {
	private int damagedIndex = 7;
	private int dieIndex = 10;

	void Start() {
		GetComponent<EnemyDie> ().SetIndex (100);
	}

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (other.CompareTag ("Player")) {
			if (GameObject.Find("TutorialPanel") != null) {
				TutorialObserver to = GameObject.Find("TutorialPanel").GetComponent<TutorialObserver> ();
				to.InitiateFlagCall (damagedIndex);
			}
		}
	}

	public void Die() {
		if (GameObject.Find("TutorialPanel") != null) {
			TutorialObserver to = GameObject.Find("TutorialPanel").GetComponent<TutorialObserver> ();
			to.InitiateFlagCall (dieIndex);
		}
	}
}
