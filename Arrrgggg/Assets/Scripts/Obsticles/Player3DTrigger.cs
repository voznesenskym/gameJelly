using UnityEngine;
using System.Collections;

public class Player3DTrigger : MonoBehaviour {

	public ParticleSystem cannonHitParticle;

	private Transform _transform;

	void Awake() {
		_transform = transform;
		if (collider) {
			collider.isTrigger = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("CannonBall")) {
			ParticleSystem ps =  (ParticleSystem)Instantiate(cannonHitParticle, _transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			StartCoroutine(DestroyParticle(ps));
		}
	}

	private IEnumerator DestroyParticle(ParticleSystem ps) {
		yield return new WaitForSeconds(1.5f);
		Destroy(ps);
	}
}
