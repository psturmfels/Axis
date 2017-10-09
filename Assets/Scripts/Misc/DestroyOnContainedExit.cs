using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContainedExit : MonoBehaviour {
	public string containerTag;

	void OnTriggerExit(Collider other) {
		if (other.CompareTag (containerTag)) {
			if (GetComponent<Rigidbody> () != null) {
				GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}
			if (GetComponent<ConstantForwardMotion> () != null) {
				GetComponent<ConstantForwardMotion> ().enabled = false;
			}
			FadeOut fo = gameObject.AddComponent<FadeOut> ();
			fo.multiColored = false;
			fo.alphaFadeRate = 0.05f;

			foreach (Transform child in transform) {
				FadeOut childFo = child.gameObject.AddComponent<FadeOut> ();
				childFo.multiColored = false;
				childFo.alphaFadeRate = 0.05f;
			}
		}
	}
}
