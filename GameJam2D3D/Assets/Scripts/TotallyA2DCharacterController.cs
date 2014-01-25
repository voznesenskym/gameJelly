using UnityEngine;
using System.Collections;

public class TotallyA2DCharacterController : MonoBehaviour {

	public Rigidbody2D playerRigidBody;

	void Update()
	{
		if(Input.GetKey(KeyCode.I))
			playerRigidBody.AddForce(new Vector2(0,10));
		if(Input.GetKey(KeyCode.K))
			playerRigidBody.AddForce(new Vector2(0,-10));
		if(Input.GetKey(KeyCode.L))
			playerRigidBody.AddForce(new Vector2(10,0));
		if(Input.GetKey(KeyCode.J))
			playerRigidBody.AddForce(new Vector2(-10,0));
	}

}
