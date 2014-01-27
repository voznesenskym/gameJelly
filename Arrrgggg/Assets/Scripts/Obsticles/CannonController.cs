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
		while (PhotonNetwork.room == null) yield return null;
		while (PhotonNetwork.isMasterClient) {
			if (t > FIRE_RATE) {
				OnFireCannonEvent();
				gameObject.GetPhotonView().RPC("PlaySound", PhotonTargets.All);
				t = 0;
			}
			t += Time.deltaTime;
			yield return null;
		}
	}

	[RPC]
	public void PlaySound() {
		audio.PlayOneShot(audio.clip);
	}
}
