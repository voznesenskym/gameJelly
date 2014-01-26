using UnityEngine;
using System.Collections;

public class DEBUG_LightsOff : MonoBehaviour {

	private const float MIN_TIME = 1.0f;
	private const float MAX_TIME = 5.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine(LightTimer());
	}

	private IEnumerator LightTimer() {
		float t = 0;
		float threshold = Random.Range(MIN_TIME, MAX_TIME);
		while (true) {
			if (t > threshold) {
				StartCoroutine( LightingManager.Instance.LerpIntensity(0.25f, false));
				t = 0;
				threshold = Random.Range(MIN_TIME, MAX_TIME);
			}
			t += Time.deltaTime;
			yield return null;
		}
	}
}
