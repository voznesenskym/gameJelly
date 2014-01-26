using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	private GameObject _gameObject;

	private const float MAX_FORCE = -500.0f;

	void Awake() {
		_gameObject = gameObject;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			for (int i = 0; i < collision.contacts.Length; ++i) {
				if (collision.contacts[i].otherCollider.gameObject.CompareTag("Player")) {
				Debug.Log("yay");
					collision.gameObject.rigidbody2D.AddForce(collision.contacts[i].normal * MAX_FORCE);
					break;
				}
			}
			if (_gameObject) PhotonNetwork.Destroy(_gameObject);
		}
	}
}
