using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour {
	public GameObject CircleAttackObject;
	private float attackSpeedModifier = 8.0f;
	private RegisterEnemyContact rec;
	private AudioClip blastClip; 

	void Start() { 
		rec = GetComponent<RegisterEnemyContact> ();
		blastClip = Resources.Load ("blast") as AudioClip;
	}

	public void StartCircleAttack(Vector3 maxDims, Vector3 growSpeeds) {
		AudioSource.PlayClipAtPoint (blastClip, Camera.main.transform.position, 0.4f);

		rec.EnableInvincible ();

		GameObject circleAttackObject = Instantiate (CircleAttackObject) as GameObject;
		CircleAttackGrow cg = circleAttackObject.GetComponent<CircleAttackGrow> ();
		cg.maxDims = maxDims;
		cg.growSpeeds = growSpeeds * attackSpeedModifier;
		circleAttackObject.transform.rotation = Quaternion.Euler (Vector3.right * 90.0f);
		circleAttackObject.transform.localScale = new Vector3 (10.0f, 60.0f, 10.0f);
		circleAttackObject.transform.position = new Vector3 (transform.position.x, transform.position.y, -250.0f) + transform.up.normalized * 80.0f;
		circleAttackObject.GetComponent<SnapToObject> ().target = gameObject;
	}
}
