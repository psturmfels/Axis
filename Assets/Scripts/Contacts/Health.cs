using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public bool isPlayer = true;
	public bool shouldRegenerate = true;
	public float regenerateRate = 0.001f;
	private string glassBreakBase = "GlassBreak";
	private float currentHealth = 1.0f;
	private DisplayFloatOnBar dfob;
	private int displayHealthIndex = 0;

	void Start() {
		dfob = GetComponent < DisplayFloatOnBar> ();
		dfob.SetDispValue (currentHealth, displayHealthIndex);
	}

	void Update () {
		if (shouldRegenerate) {
			currentHealth = Mathf.Min (currentHealth + regenerateRate, 1.0f);
		}
		dfob.SetDispValue (currentHealth, displayHealthIndex);
	}

	public void TakeDamage(float damageAmount) {
		if (currentHealth <= damageAmount) {
			currentHealth = 0.0f;
			Die ();
		} else {
			currentHealth -= damageAmount;
		}
	}

	public void Die() {
		if (isPlayer) {
			PlayRandomGlassBreak ();
			TriangleExplosion te = gameObject.AddComponent<TriangleExplosion>();
			GetComponent<MeshCollider> ().enabled = false;

			StartCoroutine(te.SplitMesh(true, transform.position));

			if (GameObject.Find ("TutorialPanel") != null) {
				TutorialObserver to = GameObject.Find ("TutorialPanel").GetComponent<TutorialObserver> ();
				if (to.GetIsDoingTutorial ()) {
					if (GameObject.Find ("ResetLevel") != null) {
						GameObject.Find ("ResetLevel").GetComponent<SceneLoader> ().StartLoadTutorialScene ();
						return;
					}
				}
			}
			if (GameObject.Find ("ResetLevel") != null) {
				GameObject.Find ("ResetLevel").GetComponent<SceneLoader> ().StartLoadMainScene ();
			}
			return;
		}
	}

	private void PlayRandomGlassBreak() {
		AudioClip glassBreak = Resources.Load (glassBreakBase + Random.Range (1, 7).ToString ()) as AudioClip;
		AudioSource.PlayClipAtPoint (glassBreak, Camera.main.transform.position, 0.1f);
	}
}
