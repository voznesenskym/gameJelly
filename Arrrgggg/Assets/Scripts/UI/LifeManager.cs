using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class LifeManager : Singleton<LifeManager> {

	public Transform leftHealth;
	public Transform rightHealth;

	public List<GameObject> leftHats;
	public List<GameObject> rightHats;

	public PhotonView view;
	public bool IsRightPlayer = false;

	private int _leftPlayerId = 0, _rightPlayerId = 0;

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

	void Update() {
		Debug.Log("left " + _leftPlayerId + " right " + _rightPlayerId);
	}

	public void RegisterPlayerLives() {
		GameObject go = GameObject.FindGameObjectsWithTag("Player").Where(p => p.GetComponent<PhotonView>().isMine == true)
			.Select(p => p).FirstOrDefault ();

		int id = go.GetInstanceID();

		if (_leftPlayerId == id || _rightPlayerId == id) return;

		view.RPC("SetPlayer", PhotonTargets.AllBuffered, new object[] {IsRightPlayer, id});
	}

	[RPC]
	public void SetPlayer(bool right, int id) {
			if (right) {
				_rightPlayerId = id;
			IsRightPlayer = true;
		} else {
				_leftPlayerId = id;
			IsRightPlayer = false;
		}
	}
}
