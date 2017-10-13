using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnHit : MonoBehaviour {
	public float splitProbability = 0.1f;
	private float splitScale = 450.0f;

	public void WasHit() {
		if (Random.Range (0.0f, 1.0f) < splitProbability) {
			GameObject newSpawned = Instantiate (gameObject, transform.position, transform.rotation);
			newSpawned.transform.localScale = Vector3.one * splitScale;
			if (newSpawned.GetComponent<AlternateTargetEnemyShip> () != null) {
				newSpawned.GetComponent<AlternateTargetEnemyShip> ().SwitchTargets ();
			} 
			if (newSpawned.GetComponent<TurnToColor> () != null && GetComponent<TurnToColor> () != null) {
				newSpawned.GetComponent<TurnToColor> ().ChangeOriginalColor (GetComponent<TurnToColor> ().GetOriginalColor ());
			}
			if (newSpawned.GetComponent<SphereEnemyContact> () != null) {
				newSpawned.GetComponent<SphereEnemyContact> ().StartInvincibility ();
			}
		}
	}
}
