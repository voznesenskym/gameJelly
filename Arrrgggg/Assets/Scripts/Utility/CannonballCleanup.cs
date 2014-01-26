using UnityEngine;
using System.Collections;

public class CannonballCleanup : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}
}
