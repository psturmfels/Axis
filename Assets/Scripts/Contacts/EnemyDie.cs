using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {
	private SpawnEnemy se; 
	private int index = -1;

	public void SetIndex(int newIndex) {
		index = newIndex;
	}

	void Start () {
		se = GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ();
	}
	
	public void Die() {
		if (index >= 0) {
			se.RegisterDeathAtIndex (index);
			index = -1;
			Destroy (gameObject);
		}
	}
}
