using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour {
	public GameObject shipResidual;
	public KeyCode dashKey;

	private InputManager im;
	private Rigidbody rb;
	private RegisterEnemyContact rec;
	private KillEnemyOnContact keom; 
	private bool isDashing = false;
	private float dashIncrement = 300.0f;
	private AudioClip warpClip;
	private AudioClip errorClip;

	private float dashMax = 1.0f;
	private float dashRemaining = 1.0f;
	private float dashConsumptionRate = 0.03f;
	private float dashRegenRate = 0.03f;
	private int warpIndex = 1;
	private DisplayFloatOnBar dfob;

	void Start() {
		im = GetComponent<InputManager> ();
		rb = GetComponent<Rigidbody> ();
		rec = GetComponent<RegisterEnemyContact> ();
		keom = GetComponent<KillEnemyOnContact> ();
		dfob = GetComponent<DisplayFloatOnBar> ();
		keom.isEnabled = false;
		warpClip = Resources.Load ("Warp") as AudioClip;
		errorClip = Resources.Load ("Error") as AudioClip;
	}

	void Update () {
		if (im.GetInputEnabled () && !isDashing && Input.GetKeyDown (dashKey)) {
			if (dashRemaining > 0.0f) {
				StartDash ();
			} else {
				AudioSource.PlayClipAtPoint (errorClip, Vector3.back * 500.0f, 0.4f);
				dfob.ErrorAtIndex (warpIndex);
			}
		} else if (isDashing && Input.GetKeyUp (dashKey)) {
			EndDash ();
		}
	}

	void FixedUpdate() {
		if (isDashing) {
			if (dashRemaining <= 0.0f) {
				EndDash ();
				return;
			}
			Instantiate (shipResidual, transform.position, transform.rotation);
			Vector3 dashDirection = transform.up.normalized * dashIncrement;
			transform.position = transform.position + dashDirection;
			dashRemaining = Mathf.Max(dashRemaining - dashConsumptionRate, 0.0f);
			dfob.SetDispValue (dashRemaining, warpIndex);
		}
	}

	void EndDash() {
		im.SetForwardMotionEnabled (true);
		isDashing = false;
		rec.DisableInvinciblePermanent ();
		keom.isEnabled = false;
	}

	public void StartDash() {
		AudioSource.PlayClipAtPoint (warpClip, Vector3.back * 500.0f, 0.6f);

		rec.EnableInvinciblePermanent ();
		keom.isEnabled = true;
		rb.velocity = Vector3.zero;
		im.SetForwardMotionEnabled (false);
		isDashing = true;
	}

	public void addToDashMeter() {
		dashRemaining = Mathf.Min (dashRemaining + dashRegenRate, dashMax);
		dfob.SetDispValue (dashRemaining, warpIndex);
	}
}
