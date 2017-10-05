using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContainedExit : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("LevelContainer")) {
			Destroy (gameObject);
		}
	}
}
