using UnityEngine;
using System.Collections;

public class AARRRR : MonoBehaviour {

	public	float	lifetime		=	4f;
	public	float	speed			=	1f;

	private	float	_lifetimeTimer	=	0f;

	// Update is called once per frame
	void Update () {
		UpdateLifetime();
		Move();
	}

	void UpdateLifetime() {
		_lifetimeTimer += Time.fixedDeltaTime;

		if (_lifetimeTimer > lifetime) {
			Destroy(gameObject);
		}
	}

	void Move() {
		transform.Translate(transform.right * speed * Time.fixedDeltaTime, Space.World);
	}
}
