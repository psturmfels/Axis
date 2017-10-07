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

	// Use this for initialization
	void Start () {
		ttc = GetComponent<TurnToColor> ();
		rb = GetComponent<Rigidbody> ();
		healthObject = GetComponent<Health> ();
	}

	void WasHit(GameObject Enemy) {
		Vector3 posDiff = transform.position - Enemy.transform.position;
		rb.AddForce (posDiff.normalized * knockbackStrength, ForceMode.Impulse);

		ttc.ChangeColor (Color.red);
		ttc.ReturnToOriginalColor();
		ScreenShakeEffect.Shake ();
		EnableInvincible ();
	}

	public void DisableInvincible() {
		isInvincible = false;
	}

	public void EnableInvincible() {
		isInvincible = true;
		Invoke ("DisableInvincible", invincibleDuration);
	}

	public void EnableInvinciblePermanent() {
		isInvincible = true;
	}

	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (!isInvincible && other.CompareTag ("Enemy")) {
			WasHit (other);
		}
	}
}
