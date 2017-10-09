using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour {
	public GameObject shipResidual;
	private TurnTowardPoint ttp;
	private InputManager im;
	private Rigidbody rb;
	private RegisterEnemyContact rec;
	private KillEnemyOnContact keom; 
	private bool isDashing = false;
	private float dashIncrement = 350.0f;
	private Vector3 dashDestination;

	void Start() {
		ttp = GetComponent<TurnTowardPoint> ();
		im = GetComponent<InputManager> ();
		rb = GetComponent<Rigidbody> ();
		rec = GetComponent<RegisterEnemyContact> ();
		keom = GetComponent<KillEnemyOnContact> ();
		keom.isEnabled = false;
	}

	void FixedUpdate() {
		if (isDashing) {
			if ((transform.position - dashDestination).magnitude < dashIncrement) {
				EndDash ();
			} else {
				Instantiate (shipResidual, transform.position, transform.rotation);
				Vector3 dashDirection = (dashDestination - transform.position).normalized * dashIncrement;
				transform.position = transform.position + dashDirection;
			}
		}
	}

	void EndDash() {
		im.SetInputEnabled (true);
		isDashing = false;
		rec.DisableInvinciblePermanent ();
		keom.isEnabled = false;
	}

	public void StartDash(GameObject projection) {
		rec.EnableInvinciblePermanent ();
		keom.isEnabled = true;
		rb.velocity = Vector3.zero;
		im.SetInputEnabled (false);
		ttp.SnapTowardPoint (projection.transform.position);
		isDashing = true;
		dashDestination = projection.transform.position;
		Destroy (projection);
	}
}
