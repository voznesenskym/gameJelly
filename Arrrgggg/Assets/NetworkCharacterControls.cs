using UnityEngine;
using System.Collections;

public class NetworkCharacterControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		if (stream.isWriting) {
			//our player. we need to send our position to network

			stream.SendNext(transform.position);
			stream.SendNext(transform.position);

		} else {
			// this is smeone else we need to recieve thier position

			transform.position = (Vector3)stream.ReceiveNext();
			transform.rotation = (Quaternion)stream.ReceiveNext();                   
		}
	}
}
