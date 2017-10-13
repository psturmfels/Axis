using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {
	public bool shouldShatter = true;
	private string glassBreakBase = "GlassBreak";
	private SpawnEnemy se; 
	private int index = -1;
	private bool isShrinking = false;
	private float shrinkRate = 0.0f;

	public void SetIndex(int newIndex) {
		index = newIndex;
	}

	void Start () {
		if (GameObject.FindGameObjectWithTag ("EnemySpawner") != null) {
			se = GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ();
		}
		shrinkRate = transform.localScale.x / 20.0f;
	}

	void FixedUpdate() {
		if (isShrinking) {
			if (transform.localScale.x < shrinkRate ||
			    transform.localScale.y < shrinkRate ||
			    transform.localScale.z < shrinkRate) {
				isShrinking = false;
				Destroy (gameObject);
			} else {
				transform.localScale = transform.localScale - Vector3.one * shrinkRate;
			}
		}
	}
	
	public void Die(Vector3 explosionPosition) {
		if (index >= 0) {
			DisableCollider ();
			if (GetComponent<Rigidbody> () != null) {
				GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}
			if (GetComponent<ConstantForwardMotion> () != null) {
				GetComponent<ConstantForwardMotion> ().speed = 0.0f; 
			}

			if (shouldShatter) {
				PlayRandomGlassBreak ();
				TriangleExplosion te = gameObject.AddComponent<TriangleExplosion> ();
				StartCoroutine (te.SplitMesh (true, explosionPosition));
			} else {
				StartShrinkDie ();
			}
				
			RegisterDeath (shouldShatter);
			index = -1;		
		}
	}
		
	public void StartShrinkDie() {
		isShrinking = true;
	}

	public void RegisterDeath(bool wasKilledByPlayer = false) {
		if (index >= 0) {
			if (se != null) {
				se.RegisterDeathAtIndex (index, wasKilledByPlayer);
			}
		}
	}

	private void PlayRandomGlassBreak() {
		AudioClip glassBreak = Resources.Load (glassBreakBase + Random.Range (1, 7).ToString ()) as AudioClip;
		AudioSource.PlayClipAtPoint (glassBreak, Vector3.back * 500.0f, 0.1f);
	}

	private void DisableCollider() {
		if (GetComponent<BoxCollider> () != null) {
			GetComponent<BoxCollider> ().enabled = false;
		} else if (GetComponent<MeshCollider> () != null) {
			GetComponent<MeshCollider> ().enabled = false;
		}
	}
}
