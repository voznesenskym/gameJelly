using UnityEngine;
using System.Collections;

public class FallTrigger : MonoBehaviour {

	public Transform[] respawnPoints;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			other.renderer.enabled = false;
			StartCoroutine(Respawn(other.gameObject));
		}
	}

	private IEnumerator Respawn(GameObject player) {
		int r = Random.Range(0, respawnPoints.Length);
		yield return new WaitForSeconds(1.0f);
		player.renderer.enabled = true;
		player.transform.position = respawnPoints[r].position;
	}
}
