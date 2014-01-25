using UnityEngine;
using System.Collections;

public class ForcePush : MonoBehaviour {

	Camera Cam3D;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.E))
		{
			Ray ray = Cam3D.ScreenPointToRay(Input.mousePosition);
			/*
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
				Debug.DrawLine(ray.origin, hit.point);
				*/
		}
	}
}
