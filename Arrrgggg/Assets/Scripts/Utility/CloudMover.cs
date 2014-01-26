using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour {

	public float direction = 1.0f;

	private Transform _transform;

	void Awake() {
		_transform = transform;
	}

	void Start() {
		StartCoroutine(TranslateCloud(_transform, direction));
	}

	private IEnumerator TranslateCloud(Transform cloud, float direction) {
		while (cloud) {
			cloud.position += new Vector3(direction, 0, 0) * Time.deltaTime * Random.Range(2.0f, 10.0f);
			yield return null;
		}
	}
}
