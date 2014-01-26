using UnityEngine;
using System.Collections;

public class IntroBoatRotation : MonoBehaviour
{
	private Transform _transform;

	void Awake()
	{
		_transform = transform;
	}

	void FixedUpdate()
	{
		Vector3 newAngle = _transform.eulerAngles;
		newAngle.x = Mathf.Sin(Time.time) * 4.0f;
		newAngle.z = Mathf.Sin(Time.time * 0.5f) * 2.0f;
		_transform.eulerAngles = newAngle;
	}
}
