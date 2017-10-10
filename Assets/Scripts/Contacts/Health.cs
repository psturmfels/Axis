using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public bool isPlayer = true;
	public bool shouldRegenerate = true;
	public float regenerateRate = 0.001f;
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
			Die ();
		} else {
			currentHealth -= damageAmount;
		}
	}

	public void Die() {
		if (isPlayer) {
			SceneManager.LoadScene ("Main");
		}
	}
}
