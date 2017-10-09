using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhostShip : MonoBehaviour {
	public KeyCode spawnKey;
	public KeyCode dashKey; 
	public GameObject ghostShipPrefab;
	public float reloadTime = 0.0f;

	private Rigidbody rb;
	private InputManager im;
	private bool canSpawn;
	private GameObject currentSpawnedShip = null;
	private CircleAttack ca;
	private DashAttack da;
	private AudioClip phase;

	// Use this for initialization
	void Start () {
		im = GetComponent<InputManager> ();
		rb = GetComponent<Rigidbody> ();
		ca = GetComponent<CircleAttack> ();
		da = GetComponent<DashAttack> ();
		canSpawn = true;
		phase = Resources.Load ("Phase") as AudioClip;
	}

	// Update is called once per frame
	void Update () {
		if (im.GetInputEnabled() && Input.GetKeyDown(spawnKey)) {
			if (canSpawn && currentSpawnedShip == null) {
				SpawnProjection ();
			} else if (currentSpawnedShip != null) {
				TeleportToProjection ();
			}
		} else if (im.GetInputEnabled() && Input.GetKeyDown(dashKey) && currentSpawnedShip != null) {
			da.StartDash (currentSpawnedShip);
		}
	}

	void SpawnProjection() {
		AudioSource.PlayClipAtPoint (phase, Camera.main.transform.position, 0.75f);
		canSpawn = false;
		Quaternion shipRotation = Quaternion.Euler (0.0f, 0.0f, transform.rotation.eulerAngles.z);
		currentSpawnedShip = Instantiate (ghostShipPrefab, transform.position, shipRotation) as GameObject;
		Invoke ("EnableSpawn", reloadTime);
	}

	void TeleportToProjection() {
		transform.position = currentSpawnedShip.transform.position;
		transform.rotation = currentSpawnedShip.transform.rotation;
		rb.velocity = Vector3.zero;
		ContinuousGrow indicatorGrow = currentSpawnedShip.GetComponentInChildren<ContinuousGrow> ();
		Vector3 desiredScale = Vector3.Scale (indicatorGrow.transform.localScale, currentSpawnedShip.transform.localScale);
		Vector3 desiredGrowSpeed = Vector3.Scale (indicatorGrow.growSpeeds, currentSpawnedShip.transform.localScale);
		ca.StartCircleAttack (desiredScale, desiredGrowSpeed);
		Destroy (currentSpawnedShip);
	}

	public void EnableSpawn() {
		canSpawn = true;
	}

	public void DisableSpawn() {
		canSpawn = false;
	}
}
