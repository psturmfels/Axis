using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhostShip : MonoBehaviour {
	public KeyCode spawnKey;
	public GameObject ghostShipPrefab;
	public float reloadTime = 0.0f;

	private Rigidbody rb;
	private InputManager im;
	private bool canSpawn;
	private GameObject currentSpawnedShip = null;

	// Use this for initialization
	void Start () {
		im = GetComponent<InputManager> ();
		rb = GetComponent<Rigidbody> ();
		canSpawn = true;
	}

	// Update is called once per frame
	void Update () {
		if (im.GetInputEnabled() && Input.GetKeyDown(spawnKey)) {
			if (canSpawn && currentSpawnedShip == null) {
				canSpawn = false;
				Quaternion shipRotation = Quaternion.Euler (0.0f, 0.0f, transform.rotation.eulerAngles.z);
				currentSpawnedShip = Instantiate (ghostShipPrefab, transform.position, shipRotation) as GameObject;
				Invoke ("EnableSpawn", reloadTime);
			} else if (currentSpawnedShip != null) {
				transform.position = currentSpawnedShip.transform.position;
				transform.rotation = currentSpawnedShip.transform.rotation;
				rb.velocity = Vector3.zero;
				Destroy (currentSpawnedShip);
			}
		}
	}

	public void EnableSpawn() {
		canSpawn = true;
	}

	public void DisableSpawn() {
		canSpawn = false;
	}
}
