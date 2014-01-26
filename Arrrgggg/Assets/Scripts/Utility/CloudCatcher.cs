using UnityEngine;
using System.Collections;

public class CloudCatcher : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("caught");
		Destroy(other.gameObject);
	}
}
