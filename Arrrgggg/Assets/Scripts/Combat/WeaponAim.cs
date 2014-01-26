using UnityEngine;
using System.Collections;

public class WeaponAim : MonoBehaviour {

	#region Variables
	public	PlatformerCharacter2D	controller;
	public	WeaponFire				weaponFire;
	public	Transform				arm;
	public	Camera					mainCamera;

	private	bool					_isFacingRight	=	true;
	private bool					_controllerConnected = false;
	#endregion

	#region Properties
	#endregion

	#region MonoBehaviour Methods
	// Use this for initialization
	void Start () {
		if (!controller)	controller	=	gameObject.GetComponentInParentRecursive<PlatformerCharacter2D>();
		if (!weaponFire)	weaponFire	=	gameObject.GetComponent<WeaponFire>();
		if (!camera)		mainCamera	=	Camera.main;
		if (!arm)			arm			=	transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 	armForward	=	arm.right;
		Vector3		toPoint		=	mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(mainCamera.transform.position.z + controller.transform.position.z))).XY ();// - arm.position.XY ();
		Vector3		toPointJoy		=	new Vector3(CrossPlatformInput.GetAxis("JoyRX"),
		                                CrossPlatformInput.GetAxis("JoyRY") * -1, 0).normalized;

		if (toPointJoy.sqrMagnitude > 0) {
			_controllerConnected = true;
			toPoint = toPointJoy;
		} 

		Vector3		fireAt;

		if ((controller.IsFacingRight	&&	!_isFacingRight) ||
		    (!controller.IsFacingRight	&&	_isFacingRight)) {
			Flip();
		}

		if (!_isFacingRight) {
			toPoint.y *= -1;
		}

		float		zRot		=	Vector3.Angle(armForward, toPoint);
					zRot		*=	Mathf.Sign(Vector3.Cross(armForward, toPoint).z);

		if (!_controllerConnected || toPointJoy.sqrMagnitude > 0) arm.right = toPoint;

		if (_isFacingRight) {
			fireAt = new Vector3(toPoint.x, toPoint.y, toPoint.z);
		} else {
			fireAt = new Vector3(toPoint.x, -toPoint.y, toPoint.z);
		}
		weaponFire.SetForward(fireAt);
	}
	#endregion

	#region WeaponAim Methods
	private void Flip() {
		Vector3 scale	=	arm.localScale;
				scale.x	*=	-1;
				scale.y	*=	-1;

		_isFacingRight	=	!_isFacingRight;
		arm.localScale	=	scale;
	}
	#endregion
}
