using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleExplosion : MonoBehaviour {
	private float explosionForceMin = 15000.0f;
	private float explosionForceMax = 35000.0f;
	private float explosionRadius = 5000.0f;
	private float fadeMin = 0.03f;
	private float fadeMax = 0.1f;

	public IEnumerator SplitMesh (bool destroy, Vector3 explosionPoint)    {

		if(GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null) {
			yield return null;
		}

		if(GetComponent<Collider>()) {
			GetComponent<Collider>().enabled = false;
		}

		Mesh M = new Mesh();
		if(GetComponent<MeshFilter>()) {
			M = GetComponent<MeshFilter>().mesh;
		}
		else if(GetComponent<SkinnedMeshRenderer>()) {
			M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
		}

		Material[] materials = new Material[0];
		if(GetComponent<MeshRenderer>()) {
			materials = GetComponent<MeshRenderer>().materials;
		}
		else if(GetComponent<SkinnedMeshRenderer>()) {
			materials = GetComponent<SkinnedMeshRenderer>().materials;
		}

		Vector3[] verts = M.vertices;
		Vector3[] normals = M.normals;
		Vector2[] uvs = M.uv;
		for (int submesh = 0; submesh < M.subMeshCount; submesh++) {

			int[] indices = M.GetTriangles(submesh);

			for (int i = 0; i < indices.Length; i += 3)    {
				Vector3[] newVerts = new Vector3[3];
				Vector3[] newNormals = new Vector3[3];
				Vector2[] newUvs = new Vector2[3];
				for (int n = 0; n < 3; n++)    {
					int index = indices[i + n];
					newVerts[n] = verts[index];
					newUvs[n] = uvs[index];
					newNormals[n] = normals[index];
				}

				Mesh mesh = new Mesh();
				mesh.vertices = newVerts;
				mesh.normals = newNormals;
				mesh.uv = newUvs;

				mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

				GameObject GO = new GameObject("Triangle " + (i / 3));
				GO.layer = LayerMask.NameToLayer("Default");
				GO.transform.position = transform.position;
				GO.transform.rotation = transform.rotation;
				GO.transform.localScale = transform.localScale;

				MeshRenderer mr = GO.AddComponent<MeshRenderer> ();
				mr.material = materials[submesh];
				GO.AddComponent<MeshFilter> ().mesh = mesh;

				Rigidbody rb = GO.AddComponent<Rigidbody> ();
				rb.AddExplosionForce(Random.Range(explosionForceMin, explosionForceMax), explosionPoint, explosionRadius);
				rb.useGravity = false;
				rb.angularVelocity = Random.insideUnitSphere * Mathf.PI;

				FadeOut fo = GO.AddComponent<FadeOut> ();
				fo.alphaFadeRate = Random.Range (fadeMin, fadeMax);
			}
		}

		if(destroy == true) {
			Destroy(gameObject);
		}
	}
}