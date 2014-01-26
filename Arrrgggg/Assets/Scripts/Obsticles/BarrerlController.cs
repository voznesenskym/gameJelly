using UnityEngine;
using System.Collections;

public class BarrerlController : Singleton<BarrerlController> {

	public Transform[] spawnPoints;
	public GameObject barrell;

	private const float MIN_SPAWN_TIME = 2.0f;
	private const float MAX_SPAWN_TIME = 10.0f;
	private const float MAX_FORCE = 300.0f;

	void Start() {
		StartCoroutine(SpawnBarrel());
	}

	private IEnumerator SpawnBarrel() {
		float t = 0;
		float spawnTimer = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
		while (true) {
			while (PhotonNetwork.room == null) {
				yield return null;
			}
			if (t > spawnTimer) {
				int i = Random.Range(0, spawnPoints.Length);
				GameObject go = (GameObject)PhotonNetwork.Instantiate("Barrel",
                                        spawnPoints[i].position,
                                        spawnPoints[i].rotation, 0);
				go.rigidbody2D.AddForce(go.transform.right * MAX_FORCE);
				StartCoroutine(CleanUp(go));
				t = 0;
				spawnTimer = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
			}
			t += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator CleanUp(GameObject go) {
		yield return new WaitForSeconds(4.0f);
		if (go) {
			PhotonNetwork.Destroy(go);
		}
	}
}
