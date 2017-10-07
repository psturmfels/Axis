using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public Scrollbar healthBar;
	public bool isPlayer = true;
	float currentHealth = 1.0f;

	void Update () {
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
