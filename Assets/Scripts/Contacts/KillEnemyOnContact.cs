using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyOnContact : MonoBehaviour {
	public bool isEnabled = true;

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (isEnabled && other.gameObject.CompareTag ("Enemy")) {
			KillEnemy (other.gameObject);
		}
	}

	void KillEnemy(GameObject Enemy) {
		Enemy.GetComponent<EnemyDie> ().Die ();
	}
}
