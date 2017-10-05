using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
	public float waitTime;

	// Use this for initialization
	void Start () {
		Invoke ("DestroyMe", waitTime);	
	}
		
	void DestroyMe() {
		Destroy (gameObject);
	}
}
