using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

	public GameObject cannonBall;
	public ParticleSystem cannonBlast;

	private const float FIRE_FORCE = 500.0f;

	private Transform _transform;
	
	void Awake() {
		_transform = transform;
	}

	void OnEnable() {
		if (CannonController.Instance) {
			CannonController.Instance.OnFireCannonEvent += FireCannon;
		}
	}

	void OnDisable() {
		if (CannonController.Instance) {
			CannonController.Instance.OnFireCannonEvent -= FireCannon;
		}
	}

	private void FireCannon() {
		cannonBlast.Play();
		if (!Network.isClient && !Network.isServer) {
			return;
		}
		GameObject go = (GameObject)PhotonNetwork.Instantiate("CannonBall", _transform.position, _transform.rotation, 0);
		go.rigidbody.AddForce(go.transform.forward * FIRE_FORCE);
	}
}
