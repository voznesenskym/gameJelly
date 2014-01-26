using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class SightController : MonoBehaviour {
	public float lossOfSightDuration = 5f;

	public void LoseSight() {
		camera.fieldOfView = 5f;
		Invoke("GainSight", lossOfSightDuration);
	}

	public void GainSight() {
		camera.fieldOfView = 60f;
	}
}
