using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemyContact : MonoBehaviour {
	private SpawnEnemy se; 
	private TurnToColor ttc; 
	private Rigidbody rb;

	private int index = -1;
	private bool isShrinkDying = false;
	private bool isShrinking = false;
	private bool isGrowing = false;
	private bool isInvincible = false;

	private float invincibleSeconds = 1.0f;
	private float shrinkRate = 15.0f;
	private float maxScale = 1500.0f;
	private float minScale = 450.0f;
	private float scaleIncrement = 150.0f;
	private float nextScaleTarget;

	private float explosionForce = 1500.0f;
	private float explosionRadius = 50000.0f;

	public void SetIndex(int newIndex) {
		index = newIndex;
	}

	void Start () {
		ttc = GetComponent<TurnToColor> ();
		rb = GetComponent<Rigidbody> ();
		if (GameObject.FindGameObjectWithTag ("EnemySpawner") != null) {
			se = GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ();
		}
	}

	void FixedUpdate() {
		if (isShrinkDying) {
			if (transform.localScale.x < shrinkRate ||
			    transform.localScale.y < shrinkRate ||
			    transform.localScale.z < shrinkRate) {
				isShrinkDying = false;
				Destroy (gameObject);
			} else {
				transform.localScale = transform.localScale - Vector3.one * shrinkRate;
			}
		} else if (isShrinking) {
			if (Mathf.Abs (transform.localScale.x - nextScaleTarget) < shrinkRate) {
				transform.localScale = new Vector3 (nextScaleTarget, nextScaleTarget, transform.localScale.z);
				isShrinking = false;
			} else {
				transform.localScale -= (Vector3.up + Vector3.right) * shrinkRate;
				if (nextScaleTarget < 750.0f) {
					transform.localScale -= Vector3.forward * shrinkRate;
				}
			}
		} else if (isGrowing) {
			if (Mathf.Abs (transform.localScale.x - nextScaleTarget) < shrinkRate) {
				transform.localScale = new Vector3 (nextScaleTarget, nextScaleTarget, transform.localScale.z);
				isGrowing = false;
			} else {
				transform.localScale += (Vector3.up + Vector3.right) * shrinkRate;
				if (nextScaleTarget <= 750.0f) {
					transform.localScale += Vector3.forward * shrinkRate;
				}
			}
		}
	}

	void Die() {
		DisableCollider ();
		StartShrinkDie ();

		if (index >= 0) {
			RegisterDeath ();
			index = -1;		
		}
	}

	void StartShrinkDie() {
		isShrinkDying = true;
	}

	public void TakeDamage(Vector3 explosionPosition) {
		if (isShrinkDying || isShrinking || isInvincible) {
			return;
		}
		isGrowing = false; 
		isInvincible = true;
		Invoke ("DisableInvincible", invincibleSeconds);
		ttc.ChangeColor (Color.red);
		ttc.ReturnToOriginalColor();

		if (transform.localScale.x <= minScale) {
			Die ();
		} else {
			nextScaleTarget = transform.localScale.x - scaleIncrement;
			isShrinking = true;
			rb.velocity = Vector3.zero;
			rb.AddExplosionForce (explosionForce, explosionPosition, explosionRadius);
		}
	}

	public void Grow() {
		if (transform.localScale.x >= maxScale || isInvincible || isShrinking || isShrinkDying || isGrowing) {
			return;
		}
		isGrowing = true;
		nextScaleTarget = transform.localScale.x + 150.0f;
	}

	void DisableInvincible () {
		isInvincible = false;
	}

	public void RegisterDeath() {
		if (index >= 0) {
			if (se != null) {
				se.RegisterDeathAtIndex (index);
				index = -1;
			}
		}
	}

	void DisableCollider() {
		if (GetComponent<SphereCollider> () != null) {
			GetComponent<SphereCollider> ().enabled = false;
		} 
	}
}
