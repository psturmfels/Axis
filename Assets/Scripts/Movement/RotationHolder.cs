using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHolder : MonoBehaviour {
	private float currentTurnAxis;
	private float currentRotationSpeed;

	public float GetCurrentTurnAxis() {
		return currentTurnAxis;
	}

	public void SetCurrentTurnAxis(float newTurnAxis) {
		currentTurnAxis = newTurnAxis;
	}

	public float GetCurrentRotationSpeed() {
		return currentRotationSpeed;
	}

	public void SetCurrentRotationSpeed(float newRotationSpeed) {
		currentRotationSpeed = newRotationSpeed;
	}
}
