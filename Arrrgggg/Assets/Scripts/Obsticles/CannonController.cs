using UnityEngine;
using System.Collections;

public class CannonController : Singleton<CannonController> {

	public delegate void OnFireCannon();
	public event OnFireCannon OnFireCannonEvent;

	private const float FIRE_RATE = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(FireCannon());
	}

	private IEnumerator FireCannon() {
		float t = 0;
		while (!PhotonNetwork.connected) yield return null;
		while (true) {
			if (t > FIRE_RATE) {
				OnFireCannonEvent();
				audio.PlayOneShot(audio.clip);
				t = 0;
			}
			t += Time.deltaTime;
			yield return null;
		}
	}
}
