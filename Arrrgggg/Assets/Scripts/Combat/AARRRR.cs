using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class AARRRR : Photon.MonoBehaviour {
	
	public	float	lifetime		=	4f;
	public	float	speed			=	100f;
	
	private	float		_lifetimeTimer	=	0f;
	private PhotonView	_view;
	
	void Awake() {
		_view	=	GetComponent<PhotonView>();	
	}

	void Start() {
		if (transform.right.x < 0) {
			Vector3 sc = transform.localScale;
			sc.y *= -1;
			transform.localScale = sc;
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLifetime();
		Move();
	}
	
	void UpdateLifetime() {
		_lifetimeTimer += Time.deltaTime;
		
		if (_lifetimeTimer > lifetime) {
			Destroy(gameObject);
		}
	}
	
	void Move() {
		transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		GameObject player	=	collision.gameObject;
		
		if (player.CompareTag("Player") && player.GetComponent<PhotonView>().isMine && !_view.isMine) {
			Camera.main.GetComponent<SightController>().LoseSight();
			_view.RPC("RemoveBullet", PhotonTargets.AllBuffered);
		}
	}

	[RPC]
	void RemoveBullet() {
		Destroy(gameObject);
	}
}
