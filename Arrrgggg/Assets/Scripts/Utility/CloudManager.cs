using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour {

	public GameObject[] clouds;
	public Transform left;
	public Transform right;

	private const float MIN_SPAWN_TIME = 0.5f;
	private const float MAX_SPAWN_TIME = 2.5f;

	void Start() {
		StartCoroutine(ScrollClouds());
	}

	private IEnumerator ScrollClouds() {
		float t = 0;
		float spawnTimer = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
		while (true) {
			if (t > spawnTimer) {
				int leftOrRight = Random.Range(0, 2);
				if (leftOrRight == 0) {
					GameObject go = (GameObject)Instantiate(clouds[Random.Range(0, clouds.Length)],
					                                        new Vector3(left.position.x, left.position.y + Random.Range(-8.0f, 15.0f), left.position.z),
					                                        Quaternion.identity);
					StartCoroutine(TranslateCloud(go.transform, 1));
				}else {
					GameObject go = (GameObject)Instantiate(clouds[Random.Range(0, clouds.Length)],
					                                        new Vector3(right.position.x, right.position.y + Random.Range(-8.0f, 15.0f), right.position.z),
					                                        Quaternion.identity);
					StartCoroutine(TranslateCloud(go.transform, -1));
				}
				t = 0;
				spawnTimer = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
			}
			t += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator TranslateCloud(Transform cloud, float direction) {
		while (cloud) {
			cloud.position += new Vector3(direction, 0, 0) * Time.deltaTime * Random.Range(2.0f, 10.0f);
			//t += Time.deltaTime;
			yield return null;
		}
	}
}
