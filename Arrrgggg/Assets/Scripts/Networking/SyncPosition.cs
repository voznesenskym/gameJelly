using UnityEngine;
using System.Collections;

public class SyncPosition : MonoBehaviour {

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
		}
		else
		{
			//Network player, receive data
			transform.position = (Vector3)stream.ReceiveNext();
		}
	}
}
