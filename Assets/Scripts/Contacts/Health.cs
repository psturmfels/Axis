using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public Scrollbar healthBar;
	public bool isPlayer = true;
	public bool shouldRegenerate = true;
	public float regenerateRate = 0.001f;
	float currentHealth = 1.0f;

	void Update () {
		if (shouldRegenerate) {
			currentHealth = Mathf.Min (currentHealth + regenerateRate, 1.0f);
		}
		healthBar.size = currentHealth;
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
