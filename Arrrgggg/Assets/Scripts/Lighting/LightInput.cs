using UnityEngine;
using System.Collections;

// update for gun when we get it in
public class LightInput : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float trigger = CrossPlatformInput.GetAxis("Fire1");
		if (trigger == 1) {
			StartCoroutine(LightingManager.Instance.LerpIntensity(0.5f, true));
		}
	}
}
