using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

	public ParticleSystem cannonHitParticle;

	private GameObject _gameObject;

	void Awake() {
		_gameObject = gameObject;
	}

	void OnCollisionEnter(Collision collision) {
		Instantiate(cannonHitParticle, collision.contacts[0].point, Quaternion.identity);
		Destroy(_gameObject);
		GameObject other = collision.gameObject;
		if (other.CompareTag("PlayerCollider")) {
			Debug.Log("Hit Player");
		}
	}
}
