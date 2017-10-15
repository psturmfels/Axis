using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public bool isPlayer = true;
	public bool shouldRegenerate = true;
	public float regenerateRate = 0.001f;
	public EnableGameOverPanel gameOverWatcher; 


	private string glassBreakBase = "GlassBreakLong";
	private float currentHealth = 1.0f;
	private DisplayFloatOnBar dfob;
	private int displayHealthIndex = 0;
	private int displayMiniHealthIndex = 3;
	private AudioClip damagedClip;

	void Start() {
		damagedClip = Resources.Load ("Damaged") as AudioClip;
		dfob = GetComponent < DisplayFloatOnBar> ();
		dfob.SetDispValue (currentHealth, displayHealthIndex);
		dfob.SetDispValue (currentHealth, displayMiniHealthIndex);
	}

	void Update () {
		if (shouldRegenerate) {
			currentHealth = Mathf.Min (currentHealth + regenerateRate * Time.timeScale , 1.0f);
		}
		dfob.SetDispValue (currentHealth, displayHealthIndex);
		dfob.SetDispValue (currentHealth, displayMiniHealthIndex);
	}

	public void TakeDamage(float damageAmount) {
		if (currentHealth <= damageAmount) {
			currentHealth = 0.0f;
			Die ();
		} else {
			if (damageAmount >= 0.0f) {
				AudioSource.PlayClipAtPoint (damagedClip, Vector3.back * 500.0f, 0.2f);
			}
			currentHealth -= damageAmount;
			currentHealth = Mathf.Min (currentHealth, 1.0f);
		}
	}

	public void Die() {
		if (isPlayer) {
			PlayRandomGlassBreak ();
			TriangleExplosion te = gameObject.AddComponent<TriangleExplosion>();
			GetComponent<MeshCollider> ().enabled = false;

			StartCoroutine(te.SplitMesh(true, transform.position));

			gameOverWatcher.StartEnablePanel ();
			return;
		}
	}

	private void PlayRandomGlassBreak() {
		AudioClip glassBreak = Resources.Load (glassBreakBase) as AudioClip;
		AudioSource.PlayClipAtPoint (glassBreak, Vector3.back * 500.0f, 0.3f);
	}
}
