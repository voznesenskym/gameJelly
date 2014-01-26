using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {

	public GameObject cannonBall;
	public ParticleSystem cannonBlast;

	private const float FIRE_RATE = 5.0f;
	private const float FIRE_FORCE = 500.0f;

	private Transform _transform;

	void Awake() {
		_transform = transform;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(FireCannon());
	}

	private IEnumerator FireCannon() {
		float t = 0;
		while (true) {
			if (t > FIRE_RATE) {
				cannonBlast.Play();
				GameObject go = (GameObject)Instantiate(cannonBall, _transform.position, _transform.rotation);
				go.rigidbody.AddForce(go.transform.forward * FIRE_FORCE);
				t = 0;
			}
			t += Time.deltaTime;
			yield return null;
		}
	}
}
