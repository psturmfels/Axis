using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientOnStart : MonoBehaviour {
	private TurnTowardPoint ttp; 

	void Start () {
		ttp = GetComponent<TurnTowardPoint> ();
		GameObject ship = GameObject.FindGameObjectWithTag ("Player");
		ttp.SnapTowardPoint (ship.transform.position);
	}
}
