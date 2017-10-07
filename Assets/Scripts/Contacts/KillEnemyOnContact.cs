using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyOnContact : MonoBehaviour {
	public bool isEnabled = true;

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (isEnabled && other.gameObject.CompareTag ("Enemy")) {
			if (coll.contacts.Length > 0) {
				KillEnemy (other.gameObject, coll.contacts[0].point);
			} else {
				KillEnemy (other.gameObject, transform.position);
			}
		}
	}

	void KillEnemy(GameObject Enemy, Vector3 explosionPosition) {
		Enemy.GetComponent<EnemyDie> ().Die (explosionPosition);
	}
}
