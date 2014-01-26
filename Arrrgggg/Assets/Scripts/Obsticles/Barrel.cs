using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	private GameObject _gameObject;

	private const float MAX_FORCE = -10000.0f;

	void Awake() {
		_gameObject = gameObject;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			collision.gameObject.rigidbody2D.AddForce(collision.contacts[0].normal * MAX_FORCE);
			if (_gameObject) PhotonNetwork.Destroy(_gameObject);
		}
	}
}
