using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeManager : Singleton<LifeManager> {

	public Transform leftHealth;
	public Transform rightHealth;

	public List<GameObject> leftHats;
	public List<GameObject> rightHats;

	public PhotonView view;
	public bool IsRightPlayer = false;

	void Start() {
		leftHats = leftHealth.gameObject.GetChildGameObjects();
		rightHats = rightHealth.gameObject.GetChildGameObjects();
	}

	public void SetLeftHealth() {
		leftHealth.gameObject.SetActive(true);
	}

	public void SetRightHealth() {
		rightHealth.gameObject.SetActive(true);
	}

	[RPC]
	public void RegisterPlayerLives() {
		IsRightPlayer = true;
	}
}
