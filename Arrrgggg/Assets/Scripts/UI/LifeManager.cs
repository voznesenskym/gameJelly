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

		view.RPC("SetPlayer", PhotonTargets.AllBuffered, new object[] {IsRightPlayer, id});
	}

	public void RemoveLifeFrom(int id) {
		view.RPC("RemoveLife", PhotonTargets.All, new object[] {id});
	}

	[RPC]
	public void SetPlayer(bool right, int id) {
		if (right) {
			_rightPlayerId = id;
			IsRightPlayer = false;
		} else {
			_leftPlayerId = id;
			IsRightPlayer = true;
		}
	}

	[RPC]
	private void RemoveLife(int id) {
		if (_leftPlayerId == id) {
			if (leftHats.Count > 0) {
				Destroy(leftHats[0]);
				leftHats.RemoveAt(0);
			}
		} else if (_rightPlayerId == id) {
			if (rightHats.Count > 0) {
				Destroy(rightHats[0]);
				rightHats.RemoveAt(0);
			}
		}
	}
}
