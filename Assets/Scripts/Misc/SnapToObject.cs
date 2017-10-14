using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToObject : MonoBehaviour {
	public GameObject target;
	public Vector3 worldOffset = Vector3.zero;
	public float localUpOffset = 80.0f;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -250.0f) + target.transform.up.normalized * localUpOffset + worldOffset;
	}
}
