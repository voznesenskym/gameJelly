using UnityEngine;
using System.Collections;

public class ForcePush : MonoBehaviour {

	public Camera Cam3D;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.E))
		{
			Ray ray = Cam3D.ScreenPointToRay(new Vector3(Screen.width/2 + Screen.width/4, Screen.height/2, 0));

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000))
			{
				Debug.DrawLine(ray.origin, hit.point);

				if(hit.rigidbody != null)
					hit.rigidbody.AddForce(Cam3D.transform.forward * 30);
			}

		}
	}
}
