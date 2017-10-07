using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {
	private SpawnEnemy se; 
	private int index;

	public void SetIndex(int newIndex) {
		index = newIndex;
	}

	void Start () {
		se = GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ();
	}
	
	public void Die() {
		se.RegisterDeathAtIndex (index);
		Destroy (gameObject);
	}
}
