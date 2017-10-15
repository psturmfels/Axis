using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterEnemyContact : MonoBehaviour {
	private TurnToColor ttc;
	private float invincibleDuration = 0.4f;
	private bool isInvincible = false;
	private float knockbackStrength = 8000.0f;
	private Rigidbody rb;
	private Health healthObject;

	private bool isAttacking = false;
	private bool godModeActive = false; 

	public FadeOut miniHealthBarBackground;
	public FadeOut miniHealthBar;
	public KeyCode godModeKey;

	// Use this for initialization
	void Start () {
		ttc = GetComponent<TurnToColor> ();
		rb = GetComponent<Rigidbody> ();
		healthObject = GetComponent<Health> ();
	}

	void Update() {
		if (Input.GetKeyDown (godModeKey)) {
			godModeActive = !godModeActive;
			if (godModeActive) {
				healthObject.TakeDamage (-1.0f);
			}
		}
	}

	void WasHit(GameObject Enemy) {
		if (miniHealthBarBackground != null) {
			miniHealthBarBackground.SetAlphaToOne ();
			miniHealthBar.SetAlphaToOne ();
		}
		Vector3 posDiff = transform.position - Enemy.transform.position;
		rb.AddForce (posDiff.normalized * knockbackStrength, ForceMode.Impulse);

		float damageTaken = Enemy.GetComponent<EnemyDamageDealt> ().damageDealt;
		if (godModeActive) {
			healthObject.TakeDamage (0.0f);
		} else {
			healthObject.TakeDamage (damageTaken);
		}

		ttc.ChangeColor (Color.red);

		ScreenShakeEffect.Shake ();
		EnableInvincible ();
	}

	public void DisableInvincible() {
		if (isAttacking) {
			return;
		}
		isInvincible = false;
	}

	public void EnableInvincible() {
		isInvincible = true;
		Invoke ("DisableInvincible", invincibleDuration);
	}

	public void DisableInvinciblePermanent() {
		isInvincible = false;
		isAttacking = false;
	}

	public void EnableInvinciblePermanent() {
		isInvincible = true;
		isAttacking = true;
	}

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (!isInvincible && other.CompareTag ("Enemy")) {
			WasHit (other);
		}
	}
	void OnCollisionStay(Collision coll) {
		GameObject other = coll.gameObject;
		if (!isInvincible && other.CompareTag ("Enemy")) {
			WasHit (other);
		}
	}
}
