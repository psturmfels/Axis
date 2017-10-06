using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour {
	public GameObject CircleAttackObject;
	private float attackSpeedModifier = 4.0f;
	private RegisterEnemyContact rec;

	void Start() { 
		rec = GetComponent<RegisterEnemyContact> ();
	}

	public void StartCircleAttack(Vector3 maxDims, Vector3 growSpeeds) {
		rec.EnableInvincible ();
		GameObject circleAttackObject = Instantiate (CircleAttackObject) as GameObject;
		ContinuousGrow cg = circleAttackObject.GetComponent<ContinuousGrow> ();
		cg.maxDims = maxDims;
		cg.growSpeeds = growSpeeds * attackSpeedModifier;
		circleAttackObject.transform.rotation = Quaternion.Euler (Vector3.right * 90.0f);
		circleAttackObject.transform.localScale = new Vector3 (10.0f, 60.0f, 10.0f);
		circleAttackObject.GetComponent<SnapToObject> ().target = gameObject;
	}
}
