﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class AARRRR : MonoBehaviour {
	
	public	float	lifetime		=	4f;
	public	float	speed			=	1f;
	
	private	float		_lifetimeTimer	=	0f;
	private NetworkView	_view;
	
	void Awake() {
		_view	=	GetComponent<NetworkView>();	
	}
	
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
	
	void OnCollisionEnter2D(Collision2D collision) {
		GameObject player	=	collision.gameObject;
		
		if (player.CompareTag("Player") && player.GetComponent<NetworkView>().isMine && _view.isMine) {
			// TODO lights out
		}
	}
}
