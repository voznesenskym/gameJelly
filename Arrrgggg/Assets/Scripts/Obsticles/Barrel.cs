using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	public Transform[] respawnPoints;

	private GameObject player;

	private const float MAX_FORCE = 10000.0f;

	private bool debounce = false;
	private Quaternion orgRot;

	void Awake() {
		player = gameObject;
		orgRot = player.transform.rotation;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("CannonBall")) {
			if (debounce) return;
			debounce = true;
			//LockPlayerInPlace();
			StartCoroutine(Floppy());
		}
	}

	private IEnumerator Floppy() {
		player.GetComponent<Platformer2DUserControl>().enabled = false;
		player.rigidbody2D.fixedAngle = false;
		player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
		yield return new WaitForSeconds(2.0f);
		player.transform.rotation = orgRot;
		player.transform.position = player.transform.position + new Vector3(0, 1, 0);
		player.rigidbody2D.fixedAngle = true;
		player.GetComponent<Platformer2DUserControl>().enabled = true;
		debounce = false;
	}

	private void LockPlayerInPlace() {
		player.GetComponent<Platformer2DUserControl>().enabled = false;
		player.rigidbody2D.Sleep();
	}

	private void LoseLife() {
		LifeManager.Instance.RemoveLifeFrom(player.GetInstanceID());
	}

	private void SplatPlayer() {
		player.rigidbody2D.fixedAngle = false;
		player.transform.Rotate(new Vector3(0, 0, 90));
	}

	private IEnumerator Respawn() {
		SplatPlayer();
		LoseLife();
		int r = Random.Range(0, respawnPoints.Length);
		yield return new WaitForSeconds(2.5f);
		player.transform.position = respawnPoints[r].position;
		player.transform.Rotate(new Vector3(0, 0, -90));
		player.rigidbody2D.fixedAngle = true;
		player.GetComponent<Platformer2DUserControl>().enabled = true;
		player.rigidbody2D.WakeUp();
		debounce = false;
	}
}
