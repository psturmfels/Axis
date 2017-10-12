using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEnemyOnContact : MonoBehaviour {
	private SphereEnemyContact sec;
	private Rigidbody rb;

	void Start() {
		sec = GetComponent<SphereEnemyContact> ();
		rb = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (other.CompareTag ("Enemy")) {
			if (other.GetComponent<EnemyDie> () != null) {
				EnemyDie otherDie = other.GetComponent<EnemyDie> ();
				otherDie.shouldShatter = false;
				otherDie.Die (Vector3.zero);
				sec.Grow ();
				rb.velocity = Vector3.zero;
			}
		}
	}
}
