using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class BlindnessMatUpdater : MonoBehaviour {

	public GameObject MaskObject;

	private float _actualRadius = 1.2f;
	private float _targetRadius = 1.2f;
	private Vector3 _springMomentum;

	void Start()
	{
		_springMomentum = new Vector3();
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward, Time.deltaTime * -600);
		//renderer.material.SetVector("_playerPos", new Vector4(transform.position.x, transform.position.y, transform.position.z, 1));
		Vector3 screenSpacePos = Camera.main.WorldToViewportPoint(transform.position);
		renderer.material.SetVector("_playerPos", new Vector4(screenSpacePos.x, screenSpacePos.y, screenSpacePos.z, 1));
		MaskObject.renderer.material.SetVector("_playerPos", new Vector4(screenSpacePos.x, screenSpacePos.y, screenSpacePos.z, 1));

		//yay hacky stealing of vector3 functions:
		Vector3 fakeV3 = new Vector3(0, _actualRadius, 0);
		Vector3 fakeV3Target = new Vector3(0, _targetRadius, 0);

		fakeV3 = Vector3.SmoothDamp(fakeV3, fakeV3Target, ref _springMomentum, 0.25f);
		_actualRadius = fakeV3.y;

		renderer.material.SetFloat("_ClearRadius", _actualRadius);
		MaskObject.renderer.material.SetFloat("ClearRadius", _actualRadius);
	}

	public void SetScreenObscuredPercentage(float obscuration)
	{
		_targetRadius = obscuration;
	}

	public float GetScreenObscuredPercentage() {
		return _targetRadius; 
		//renderer.material.GetFloat("_ClearRadius");
	}
}
	