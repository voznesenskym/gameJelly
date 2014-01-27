using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

	public ParticleSystem cannonBlast;

	private const float FIRE_FORCE = 500.0f;

	private Transform _transform;
	private GameObject _go;
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

		_go = (GameObject)PhotonNetwork.Instantiate("CannonBall", _transform.position, _transform.rotation, 0);
		gameObject.GetPhotonView().RPC("ReallyFireCannon", PhotonTargets.All);
	}

	[RPC]
	public void ReallyFireCannon() {
		_go.rigidbody.AddForce(_go.transform.forward * FIRE_FORCE);
	}
}
