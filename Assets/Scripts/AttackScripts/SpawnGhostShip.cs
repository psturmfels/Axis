﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhostShip : MonoBehaviour {
	public KeyCode spawnKey;
	public GameObject ghostShipPrefab;
	private float reloadTime = 0.0f;

	private Rigidbody rb;
	private InputManager im;
	private bool canSpawn;
	private GameObject currentSpawnedShip = null;
	private CircleAttack ca;
	private AudioClip phase;
	private AudioClip errorClip;
	private AudioClip ready;

	private float spawnMeterMax = 1.0f;
	private float spawnMeterRemaining = 1.0f;
	private float spawnMeterRegenRate = 0.005f;
	private int spawnMeterIndex = 2;
	private DisplayFloatOnBar dfob;


	// Use this for initialization
	void Start () {
		im = GetComponent<InputManager> ();
		rb = GetComponent<Rigidbody> ();
		ca = GetComponent<CircleAttack> ();
		dfob = GetComponent<DisplayFloatOnBar> ();
		canSpawn = true;
		phase = Resources.Load ("Phase") as AudioClip;
		errorClip = Resources.Load ("Error") as AudioClip;
		ready = Resources.Load ("Ready") as AudioClip;
	}

	// Update is called once per frame
	void Update () {
		if (im.GetInputEnabled () && Input.GetKeyDown (spawnKey)) {
			if (canSpawn && currentSpawnedShip == null && spawnMeterRemaining == spawnMeterMax) {
				SpawnProjection ();
				spawnMeterRemaining = 0.0f;
			} else if (currentSpawnedShip != null) {
				TeleportToProjection ();
			} else {
				AudioSource.PlayClipAtPoint (errorClip, Vector3.back * 500.0f, 0.4f);
				dfob.ErrorAtIndex (spawnMeterIndex);
			}
		} else {
			if (spawnMeterRemaining < 1.0f && spawnMeterRemaining + spawnMeterRegenRate >= 1.0f) {
				AudioSource.PlayClipAtPoint (ready, Vector3.back * 500.0f, 0.35f);
			}
			spawnMeterRemaining = Mathf.Min (spawnMeterMax, spawnMeterRemaining + spawnMeterRegenRate * Time.timeScale);
			dfob.SetDispValue (spawnMeterRemaining, spawnMeterIndex);
		}
	}

	void SpawnProjection() {
		AudioSource.PlayClipAtPoint (phase, Vector3.back * 500.0f, 0.65f);
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
