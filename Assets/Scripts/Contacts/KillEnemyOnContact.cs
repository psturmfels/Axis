using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyOnContact : MonoBehaviour {
	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (other.gameObject.CompareTag ("Enemy")) {
			KillEnemy (other.gameObject);
		}
	}

	void KillEnemy(GameObject Enemy) {
		Destroy (Enemy);
	}
}
