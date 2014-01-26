using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class BlindnessMatUpdater : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward, Time.deltaTime * -600);
		//renderer.material.SetVector("_playerPos", new Vector4(transform.position.x, transform.position.y, transform.position.z, 1));
		Vector3 screenSpacePos = Camera.main.WorldToViewportPoint(transform.position);
		renderer.material.SetVector("_playerPos", new Vector4(screenSpacePos.x, screenSpacePos.y, screenSpacePos.z, 1));
	}

	public void SetScreenObscuredPercentage(float obscuration)
	{
		renderer.material.SetFloat("_ClearRadius", obscuration);
	}
}
	