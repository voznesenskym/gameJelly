using UnityEngine;
using System.Collections;

public class FallTrigger : MonoBehaviour {

	public Transform[] respawnPoints;

	private GameObject[] playerPool = new GameObject[2];

	void Awake() {
		for (int i = 0; i < playerPool.Length; ++i) {
			playerPool[i] = null;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		int currentIndex = -1;
		if (other.CompareTag("Player")) {
			for (int i = 0; i < playerPool.Length; ++i) {
				if (!playerPool[i]) {
					currentIndex = i;
					playerPool[i] = other.gameObject;
					break;
				}
			}
			if (currentIndex > -1 ) {
				LoseLife(other.gameObject);
				LockPlayerInPlace(other.gameObject);
				StartCoroutine(Respawn(other.gameObject, currentIndex));
			}
		}
	}

	private void LockPlayerInPlace(GameObject go) {
		go.GetComponent<Platformer2DUserControl>().enabled = false;
		go.rigidbody2D.Sleep();
	}

	private void RendererSwitch(Transform t, bool isOn) {
		t.renderer.enabled = isOn;
		for (int i = 0; i < t.childCount; ++i) {
			if (t.GetChild(i).renderer) {
				t.GetChild(i).renderer.enabled = isOn;
				RendererSwitch(t.GetChild(i), isOn);
			}
		}
	}

	private void LoseLife(GameObject player) {
		LifeManager.Instance.RemoveLifeFrom(player.GetInstanceID());
	}

	private IEnumerator Respawn(GameObject player, int index) {
		int r = Random.Range(0, respawnPoints.Length);
		player.transform.position = respawnPoints[r].position;
		player.GetComponent<Platformer2DUserControl>().enabled = true;
		player.rigidbody2D.WakeUp();
		yield return new WaitForSeconds(1.0f);
		playerPool[index] = null;
	}
}
