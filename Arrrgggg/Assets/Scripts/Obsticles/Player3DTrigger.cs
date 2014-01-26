using UnityEngine;
using System.Collections;

public class Player3DTrigger : MonoBehaviour {

	public string cannonHitParticle = "CannonPlayerHit";
	public Transform[] respawnPoints;
	public GameObject player;

	private Transform _transform;
	private int particleCount;

	void Awake() {
		_transform = transform;
		if (collider) {
			collider.isTrigger = true;
		}
		particleCount = 0;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("CannonBall")) {
			GameObject ps =  (GameObject)PhotonNetwork.Instantiate(cannonHitParticle, _transform.position, Quaternion.identity, 0);
			ParticleSystem p = ps.particleSystem; 
			particleCount ++;
			StartCoroutine(DestroyParticle(p));
			if (other && other.gameObject) PhotonNetwork.Destroy(other.gameObject);
			//Destroy(other.gameObject);
			LockPlayerInPlace();
			StartCoroutine(Respawn());
		}
	}

	private IEnumerator DestroyParticle(ParticleSystem ps) {
		yield return new WaitForSeconds(1.5f);
		particleCount --;
		if (ps && ps.gameObject) PhotonNetwork.Destroy(ps.gameObject);
	}

	private void LockPlayerInPlace() {
		Debug.Log("lock");
		player.GetComponent<Platformer2DUserControl>().enabled = false;
		player.rigidbody2D.Sleep();
	}
	
	private IEnumerator SplatPlayer() {
		Debug.Log("Splat");
		float t = 0;
		float duration = 2;
		while (t < duration) {
			player.transform.position += new Vector3(0, 3, -6) * Time.deltaTime;
			t += Time.deltaTime;
			yield return null;
		}
	}
	
	private void LoseLife() {
		LifeManager.Instance.RemoveLifeFrom(player.GetInstanceID());
	}
	
	private IEnumerator Respawn() {
		yield return StartCoroutine(SplatPlayer());
		LoseLife();
		int r = Random.Range(0, respawnPoints.Length);
		yield return new WaitForSeconds(1.0f);
		player.transform.position = respawnPoints[r].position;
		player.GetComponent<Platformer2DUserControl>().enabled = true;
		player.rigidbody2D.WakeUp();
	}
}
