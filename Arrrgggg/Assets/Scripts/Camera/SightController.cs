using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class SightController : MonoBehaviour {
	public float lossOfSightDuration = 5f;

	public void LoseSight() {
		camera.enabled = false;
		Invoke("GainSight", lossOfSightDuration);
	}

	public void GainSight() {
		camera.enabled = true;
	}
}
