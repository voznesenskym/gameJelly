using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

	public GameObject grabGuide;

	private bool _currentlyHoldingSomething;
	private Transform _heldThing;

	void FixedUpdate() {
		if(_currentlyHoldingSomething)
		{
			_heldThing.rigidbody.AddForce((grabGuide.transform.position - _heldThing.position) * 100);
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			if(_currentlyHoldingSomething)
			{
				_heldThing.rigidbody.useGravity = true;
				//_heldThing.gameObject.AddComponent<Rigidbody>();
				_heldThing.parent = null;
				_currentlyHoldingSomething = false;
			}
			else
			{
				Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2 + Screen.width/4, Screen.height/2, 0));

				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 10000))
				{
					Debug.DrawLine(ray.origin, hit.point);

					if(hit.rigidbody != null && hit.transform.gameObject.tag.Equals("DragDroppable"))
					{
						_heldThing = hit.transform;
						_heldThing.parent = transform;
						_heldThing.rigidbody.useGravity = false;
						//Destroy(_heldThing.rigidbody);
						_currentlyHoldingSomething = true;
					}
				}
			}
		}
	}
}
