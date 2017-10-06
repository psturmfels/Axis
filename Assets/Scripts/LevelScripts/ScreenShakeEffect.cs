using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeEffect : MonoBehaviour {

	public static ScreenShakeEffect instance;
	private Vector3 localReturnPosition;

	// Use this for initialization
	void Start () {
		if (instance != null && instance != this)
		{
			Destroy(this);
			return;
		}
		else
			instance = this;
		localReturnPosition = transform.position;
	}

	public static void Shake(float duration = 0.3f, float radius = 75.0f) {
		instance.StartCoroutine(instance.ShakeEffect(duration, radius));
	}

	public IEnumerator ShakeEffect(float duration, float radius) {
		for (float t = 0; t < duration; t += Time.deltaTime) {
			Vector2 unitCircleRand = UnityEngine.Random.insideUnitCircle;
			transform.localPosition = new Vector3(unitCircleRand.x, unitCircleRand.y, 0.0f) * radius + localReturnPosition;
			yield return null;
		}
		transform.localPosition = localReturnPosition;
	}
}
