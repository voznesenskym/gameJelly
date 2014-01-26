using UnityEngine;
using System.Collections;

// change as needed once networking goes in
public class LightingManager : Singleton<LightingManager> {

	private const float MIN_INTENSITY = 0.0f;
	private const float MAX_INTENSITY = 8.0f;

	private Light _light;
	private bool _isActive;

	void Awake() {
		_light = Camera.main.light;
	}

	public IEnumerator LerpIntensity(float rate, bool isOn) {
		while (_isActive) {
			Debug.Log("Waiting");
			yield return null;
		}
		_isActive = true;
		float start = isOn ? _light.intensity : MAX_INTENSITY;
		float finish = isOn ? MAX_INTENSITY : MIN_INTENSITY;
		if (!isOn && _light.intensity == MIN_INTENSITY) {
			_isActive = false;
			yield break;
		}
		float t = 0;
		while (t < 1) {
			_light.intensity = Mathf.Lerp(start, finish, t);
			t += Time.deltaTime / rate;
			yield return null;
		}

		_light.intensity = finish;
		_isActive = false;
	}
}
