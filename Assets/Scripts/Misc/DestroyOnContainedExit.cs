using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContainedExit : MonoBehaviour {
	public string containerTag;

	void OnTriggerExit(Collider other) {
		if (other.CompareTag (containerTag)) {
			Destroy (gameObject);
		}
	}
}
