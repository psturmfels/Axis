using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyOnContact : MonoBehaviour {
	public bool isEnabled = true;
	private DashAttack da;

	void Awake() {
		da = GameObject.FindGameObjectWithTag ("Player").GetComponent<DashAttack> ();
	}

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
		da.addToDashMeter ();
		if (Enemy.GetComponent<RedCubeFlagCall> () != null) {
			Enemy.GetComponent<RedCubeFlagCall> ().Die ();
		}
		if (Enemy.GetComponent<EnemyDie> () != null) {
			Enemy.GetComponent<EnemyDie> ().Die (explosionPosition);
		} else if (Enemy.GetComponent<SphereEnemyContact> () != null) {
			Enemy.GetComponent<SphereEnemyContact> ().TakeDamage (explosionPosition);
		}
	}
}
