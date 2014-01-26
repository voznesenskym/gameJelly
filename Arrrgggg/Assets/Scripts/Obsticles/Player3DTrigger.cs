﻿using UnityEngine;
using System.Collections;

public class Player3DTrigger : MonoBehaviour {

	public string cannonHitParticle = "CannonPlayerHit";

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
			if (particleCount < 4){
				Debug.Log ("inside particle count");
				GameObject ps =  (GameObject)PhotonNetwork.Instantiate(cannonHitParticle, _transform.position, Quaternion.identity, 0);
				ParticleSystem p = ps.particleSystem; 
				particleCount ++;
				StartCoroutine(DestroyParticle(p));
			}
			PhotonNetwork.Destroy(other.gameObject);
			//Destroy(other.gameObject);

		}
	}

	private IEnumerator DestroyParticle(ParticleSystem ps) {
		yield return new WaitForSeconds(1.5f);
		particleCount --;
		Destroy(ps);
	}
}
