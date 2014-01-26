using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour {

	public  Transform	bulletSpawnPoint;
	public  GameObject	bullet;
	public  float		cooldown		=	2f;

	private Vector3		_forward;
	private float		_cooldownTimer	=	0f;
	private	bool		_isOnCooldown	=	false;

	public bool IsOnCooldown {
		get { return _isOnCooldown; }
	}

	// Use this for initialization
	void Start () {
		if (!bulletSpawnPoint)	bulletSpawnPoint		=	transform;
		if (!bullet)			bullet					=	(GameObject)Resources.Load("AARRRR");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && !_isOnCooldown) {
			FireBullet();
		}

		if (_isOnCooldown) {
			UpdateCooldown();
		}
	}

	public void SetForward(Vector3 forward) {
		_forward = forward;
	}

	private void FireBullet() {
		GameObject b = (GameObject)Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		b.transform.right	=	_forward;

		_isOnCooldown			=	true;
	}

	private void UpdateCooldown() {
		_cooldownTimer	+=	Time.fixedDeltaTime;
		if (_cooldownTimer > cooldown) {
			OffCooldown();
		}
	}

	private void OffCooldown() {
		_cooldownTimer	=	0f;
		_isOnCooldown	=	false;
	}
}
