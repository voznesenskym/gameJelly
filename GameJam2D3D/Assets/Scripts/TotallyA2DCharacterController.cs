using UnityEngine;
using System.Collections;

public class TotallyA2DCharacterController : MonoBehaviour {

	public Rigidbody2D playerRigidBody;
	float timeTillNextJump;

	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.A))
			playerRigidBody.AddForce(new Vector2(-10,0));
		if(Input.GetKey(KeyCode.D))
			playerRigidBody.AddForce(new Vector2(10,0));
	}

	void OnCollisionStay2D(Collision2D collisionData)
	{
		Vector2 averageAwayFromSurfaceVector = Vector2.zero;
		foreach (ContactPoint2D contact in collisionData.contacts)
		{
			averageAwayFromSurfaceVector += contact.normal;
		}

		averageAwayFromSurfaceVector = (averageAwayFromSurfaceVector / (float)collisionData.contacts.Length);

		//make it so we're at least always jumping somewhat up:
		averageAwayFromSurfaceVector = (averageAwayFromSurfaceVector + Vector2.up) * 100;

		if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
			playerRigidBody.AddForce(averageAwayFromSurfaceVector);
		//if(Input.GetKey(KeyCode.S))
	//		playerRigidBody.AddForce(new Vector2(0,-10));
	}

}
