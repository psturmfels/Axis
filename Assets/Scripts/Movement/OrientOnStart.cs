using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientOnStart : MonoBehaviour {
	private TurnTowardPoint ttp; 

	void Start () {
		ttp = GetComponent<TurnTowardPoint> ();
		GameObject ship = GameObject.FindGameObjectWithTag ("Player");
		if (ship == null) {
			return;
		}
		ttp.SnapTowardPoint (ship.transform.position);
	}
}
