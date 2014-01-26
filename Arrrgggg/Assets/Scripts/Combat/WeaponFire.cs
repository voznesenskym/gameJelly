using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour {

	public Transform	arm;
	public Transform	bulletSpawnPoint;
	public GameObject	bullet;

	// Use this for initialization
	void Start () {
		if (!bulletSpawnPoint)	bulletSpawnPoint		=	transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			FireBullet();
		}
	}

	private void FireBullet() {
	}
}
