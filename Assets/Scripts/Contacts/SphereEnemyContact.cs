using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereEnemyContact : MonoBehaviour {
	public int deathScore;

	private SpawnEnemy se; 
	private TurnToColor ttc; 
	private Rigidbody rb;
	private SplitOnHit soh;
	private AudioClip grow;
	private AudioClip SphereHurt;
	private AudioClip SphereDie; 


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
		grow = Resources.Load ("Grow") as AudioClip;
		SphereHurt = Resources.Load ("SphereHurt") as AudioClip;
		SphereDie = Resources.Load ("SphereDie") as AudioClip;
		ttc = GetComponent<TurnToColor> ();
		rb = GetComponent<Rigidbody> ();
		soh = GetComponent<SplitOnHit> ();
		if (GameObject.FindGameObjectWithTag ("EnemySpawner") != null) {
			se = GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ();
		}
	}

	void FixedUpdate() {
		if (isShrinkDying) {
			if (transform.localScale.x < shrinkRate ||
			    transform.localScale.y < shrinkRate ||
			    transform.localScale.z < shrinkRate) {
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
		displayScoreText ();
		DisableCollider ();
		StartShrinkDie ();
		AudioSource.PlayClipAtPoint (SphereDie, Vector3.back * 500.0f, 0.6f);

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
		StartInvincibility ();
		ttc.ChangeColor (Color.red);

		if (transform.localScale.x <= minScale) {
			Die ();
		} else {
			AudioSource.PlayClipAtPoint (SphereHurt, Vector3.back * 500.0f, 0.6f);
			soh.WasHit ();
			nextScaleTarget = transform.localScale.x - scaleIncrement;
			isShrinking = true;
			rb.velocity = Vector3.zero;
			rb.AddExplosionForce (explosionForce, explosionPosition, explosionRadius);
		}
	}

	public void StartInvincibility() {
		isInvincible = true;
		Invoke ("DisableInvincible", invincibleSeconds);
	}

	public void Grow() {
		if (transform.localScale.x >= maxScale || isInvincible || isShrinking || isShrinkDying || isGrowing) {
			return;
		}
		isGrowing = true;
		AudioSource.PlayClipAtPoint (grow, Vector3.back * 500.0f, 0.6f);
		nextScaleTarget = transform.localScale.x + 150.0f;
	}

	void DisableInvincible () {
		isInvincible = false;
	}

	public void RegisterDeath() {
		if (index >= 0) {
			if (se != null) {
				se.reduceNumSpheres ();
				se.RegisterDeathAtIndex (index, deathScore, true);
				index = -1;
			}
		}
	}

	void DisableCollider() {
		if (GetComponent<SphereCollider> () != null) {
			GetComponent<SphereCollider> ().enabled = false;
		} 
	}

	private void displayScoreText() {
		GameObject fadeTextPrefab = Resources.Load ("FadeText") as GameObject;
		Vector3 textPosition = new Vector3 (transform.position.x + 100.0f, transform.position.y + 100.0f, -450.0f);
		GameObject WorldCanvas = GameObject.Find ("WorldCanvas");
		GameObject fadeText = Instantiate (fadeTextPrefab, textPosition, Quaternion.identity, WorldCanvas.transform);
		fadeText.GetComponent<Text> ().text = "+" + deathScore.ToString ();
	}
}
