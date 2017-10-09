using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour {
	private string glassBreakBase = "GlassBreak";
	private SpawnEnemy se; 
	private int index = -1;

	public void SetIndex(int newIndex) {
		index = newIndex;
	}

	void Start () {
		se = GameObject.FindGameObjectWithTag ("EnemySpawner").GetComponent<SpawnEnemy> ();
	}
	
	public void Die(Vector3 explosionPosition) {
		if (index >= 0) {
			PlayRandomGlassBreak ();
			TriangleExplosion te = gameObject.AddComponent<TriangleExplosion>();
			DisableCollider ();

			StartCoroutine(te.SplitMesh(true, explosionPosition));

			se.RegisterDeathAtIndex (index);
			index = -1;		
		}
	}

	private void PlayRandomGlassBreak() {
		AudioClip glassBreak = Resources.Load (glassBreakBase + Random.Range (1, 7).ToString ()) as AudioClip;
		AudioSource.PlayClipAtPoint (glassBreak, Camera.main.transform.position, 0.1f);
	}

	private void DisableCollider() {
		if (GetComponent<BoxCollider> () != null) {
			GetComponent<BoxCollider> ().enabled = false;
		}
	}
}
