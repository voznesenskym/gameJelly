using UnityEngine;
using System.Collections;

public class CannonballCleanup : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other && other.gameObject) PhotonNetwork.Destroy(other.gameObject);
	}
}
