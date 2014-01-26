using UnityEngine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class SightController : MonoBehaviour {
	public float lossOfSightDuration = 5f, goodSightDuration = 1f;
	public float noSight = 0, normalSight = 0.7f, goodSight = 0.9f;
	public GameObject player;
	public BlindnessMatUpdater mat;

	void Update() {
		if (!player) {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			if (players != null && players.Length > 0) player = players.Where(p => p.GetComponent<PhotonView>() != null && p.GetComponent<PhotonView>().isMine == true).Select(p => p).FirstOrDefault();
			if (player) mat = player.GetComponentInChildren<BlindnessMatUpdater>();
		}
	}

	public void LoseSight() {
		if (player && mat.GetScreenObscuredPercentage() > 0.45f) {
			mat.SetScreenObscuredPercentage(noSight);
			Invoke("GainSight", lossOfSightDuration);
		}
	}

	public void GainSight() {
		mat.SetScreenObscuredPercentage(normalSight);
	}

	public void GoodSight() {
		if (player && mat.GetScreenObscuredPercentage() > 0.45f) {
			mat.SetScreenObscuredPercentage(goodSight);
			Invoke("GainSight", goodSightDuration);
		}
	}
}
