using UnityEngine;
using System.Collections;

public class WeaponAim : MonoBehaviour {

	#region Variables
	public	PlatformerCharacter2D	controller;
	public	Camera					mainCamera;
	public	Transform				arm;

	private	bool					_isFacingRight	=	true;
	#endregion

	#region Properties
	#endregion

	#region MonoBehaviour Methods
	// Use this for initialization
	void Start () {
		if (!controller)	gameObject.GetComponentInParentRecursive<PlatformerCharacter2D>();
		if (!camera)		mainCamera	=	Camera.main;
		if (!arm)			arm			=	transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 	armForward	=	arm.right;
		Vector3		toPoint		=	mainCamera.ScreenToWorldPoint(Input.mousePosition).XY() - arm.position;

		if (!_isFacingRight) {
			toPoint.y *= -1;
		}
		
		float		zRot		=	Vector3.Angle(armForward, toPoint);
					zRot		*=	Mathf.Sign(Vector3.Cross(armForward, toPoint).z);

		arm.Rotate(0, 0, _isFacingRight ? zRot : zRot);
		//arm.Rotate(0, 0, zRot);

		if ((controller.IsFacingRight	&&	!_isFacingRight) ||
		    (!controller.IsFacingRight	&&	_isFacingRight)) {
			Flip();
		}
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
